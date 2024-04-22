using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private HealthBar healthBarEnemy;

    private int health;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void TakeDamage()
    {
        health--;
        
        if (health <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject); 
        }
        else
        {
            StartCoroutine(Blink(0.3f));
            healthBarEnemy.UpdateHealthbar(maxHealth, health);
        }


    }

    private IEnumerator Blink(float blinkTime)
    {
        // Cambiar al rojo
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(blinkTime);
        spriteRenderer.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Arrow arrow = collision.gameObject.GetComponent<Arrow>();
        if(arrow != null)
        {
            TakeDamage();
        }

    }
}
