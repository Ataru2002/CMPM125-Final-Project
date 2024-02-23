using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float followSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 camNewPos = Vector2.MoveTowards(transform.position, playerTransform.position, followSpeed * Time.deltaTime);
        transform.position = new Vector3(camNewPos.x, camNewPos.y, transform.position.z);
    }
}
