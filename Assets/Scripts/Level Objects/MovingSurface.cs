using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSurface : MonoBehaviour
{
    [SerializeField] Vector2 positionA;
    [SerializeField] Vector2 positionB;
    [SerializeField] float moveDuration;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position = Vector2.Lerp(positionA, positionB, timer / moveDuration);

        if (timer >= moveDuration)
        {
            timer = 0;
            Vector2 swap = positionA;
            positionA = positionB;
            positionB = swap;
        }
    }
}
