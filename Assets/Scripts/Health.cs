using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private HealthBar healthBarEnemy;
    [SerializeField] private AudioClip enemyTakeDamageClip;
    [SerializeField] private AudioClip enemyDieClip;

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
            // Animacion muerte
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            // Sonido muerte enemigo
            AudioManager.Instance.PlaySoundEffect(enemyDieClip, 1f);
            // Eliminar objeto enemigo
            Destroy(gameObject);
            GameManager.Instance.DecreaseEnemiesLeft();


        }
        else
        {
            // Sonido daño
            AudioManager.Instance.PlaySoundEffect(enemyTakeDamageClip, 1f);

            // Parpadeo rojo y barra vida
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
