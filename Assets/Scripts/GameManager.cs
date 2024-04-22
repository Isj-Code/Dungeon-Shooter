using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsPlayerDead { get; private set; }
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Die()
    {
        // Parar el movimiento
        EnemyMovement[] listOfEnemies = FindObjectsOfType<EnemyMovement>();
        foreach (var enemy in listOfEnemies)
        {
            enemy.Stop();
        }

        // Parar el spawn de mobs
        FindObjectOfType<Spawner>().StopAllCoroutines();
        isPlayerDead = true;
    }
}
