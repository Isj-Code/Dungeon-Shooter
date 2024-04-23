using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] listSpawns;
    [SerializeField] private int waves, enemiesPerWave;
    [SerializeField] private float timeToSpan, timBetweenWaves;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int x = 0; x < waves; x++)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                var index = Random.Range(0, listSpawns.Length);
                yield return new WaitForSeconds(timeToSpan);
                Instantiate(enemyPrefab, listSpawns[index].position, Quaternion.identity);
                GameManager.Instance.IncreaseEnemiesLeft();
            }
            if( x < waves - 1)
            {
                // El paso de nivel, si estamos esperando no funcionaria
                yield return new WaitForSeconds(timBetweenWaves);
            }  
        }

        GameManager.Instance.AllWavesSpawned();
    }
}
