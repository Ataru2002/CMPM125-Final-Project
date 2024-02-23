using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.triggerFlag = true;
            playerHealth.spikeDamage = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.triggerFlag = false;
        }
    }
}
