using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public UIHealth currentTextHealth;
    public int maxHealth = 10;
    private int currentHealth;

    public float invulnerableTime = 2f;
    public bool isInvulnerable = false;

    public bool triggerFlag = false;

    public int spikeDamage;
    
    void Start()
    {
        currentHealth = maxHealth;
        currentTextHealth = GetComponent<UIHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerFlag){
            takeDamage(spikeDamage);
        }
    }

    public void takeDamage(int damage){
        if(!isInvulnerable){
            currentHealth -= damage;
            currentTextHealth.updateScore(currentHealth);
            StartCoroutine(invulnerable());
            print("Health remaining: " + currentHealth);
            if(currentHealth == 0){
                die();
            }
        }

        
    }
    public IEnumerator invulnerable(){
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        isInvulnerable = false;
    }   
    void die(){
        print("You Died!");
        LevelManager.instance.OnPlayerDeath();
    }
}
