using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem hitParticles;
    public ParticleSystem healthBarParticles;
    public HealthBar healthBar;
    const int maxHealth = 5;
    private int currentHealth;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    const float invulnerableTime = 1.4f;
    public bool isInvulnerable = false;

    public bool triggerFlag = false;

    public int spikeDamage;
    
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            healthBar.updateBar(currentHealth, maxHealth);
            hitParticles.Emit(5);
            healthBarParticles.Emit(5);
            StartCoroutine(invulnerable());
            StartCoroutine(Flash());
            print("Health remaining: " + currentHealth);
            if(currentHealth == 0){
                die();
            }

            int direction = spriteRenderer.flipX ? 1 : -1;
            rb.AddForce(new Vector2(direction * 50, 200));
        }
    }

    public IEnumerator invulnerable(){
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        isInvulnerable = false;
    }

    public IEnumerator Flash()
    {
        int phase = 0;
        while (isInvulnerable)
        {
            yield return new WaitForSeconds(0.2f);
            float alpha = phase % 2 == 0 ? 0.5f : 1.0f;
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            phase += 1;
        }
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void die(){
        print("You Died!");
        LevelManager.instance.OnPlayerDeath();
    }
}
