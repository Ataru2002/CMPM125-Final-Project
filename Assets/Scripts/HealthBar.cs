using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update

    private RectTransform image;
    Vector3 originalPosition;
    bool barShaking = false;

    private float defaultSize; 
    void Start()
    {
        image = GetComponent<RectTransform>();
        originalPosition = image.transform.localPosition;
        print("Original pos = " + originalPosition);
        defaultSize = image.sizeDelta.x;
    }

    public void updateBar(int healthPoint, int maxHealth){
        float healthBarSizeX = ((float) healthPoint / maxHealth) * defaultSize;
        image.sizeDelta = new Vector2(healthBarSizeX, image.sizeDelta.y);
    }

    public void ShakeBar()
    {
        if (!barShaking)
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        barShaking = true;
        for (int i = 0; i < 10; i++)
        {
            transform.localPosition = (Vector2)originalPosition + Random.insideUnitCircle * 5f; ;
            yield return new WaitForSeconds(0.1f);
        }
        transform.localPosition = originalPosition;
        barShaking = false;
    }
}
