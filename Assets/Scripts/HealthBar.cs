using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update

    private RectTransform image;

    private float defaultSize; 
    void Start()
    {
        image = GetComponent<RectTransform>();
        defaultSize = image.sizeDelta.x;
        
    }

    public void updateBar(int healthPoint, int maxHealth){
        float healthBarSizeX = ((float) healthPoint / maxHealth) * defaultSize;
        image.sizeDelta = new Vector2(healthBarSizeX, image.sizeDelta.y);
    }
}
