using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int lives;
    [SerializeField] private TMP_Text liveText;
    [SerializeField] private AudioClip dieAudioClip;

    public bool IsPlayerDead { get; private set; }
    public static GameManager Instance { get; private set; }
    private float restartTime = 1.0f;

    private int enemiesLeft;
    private bool allWavesSpawned;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        UpdateLiveText();

    }
    private void UpdateLiveText()
    {
        liveText.text = $"Lives {lives}";
    }

    public void Die()
    {
        // Parar el movimiento
        EnemyMovement[] listOfEnemies = FindObjectsOfType<EnemyMovement>();
        foreach (var enemy in listOfEnemies)
        {
            enemy.Stop();
        }
        // Sonido de muerte
        AudioManager.Instance.PlaySoundEffect(dieAudioClip, 1f);

        // Parar el spawn de mobs
        FindObjectOfType<Spawner>().StopAllCoroutines();
        IsPlayerDead = true;
        lives--;
        UpdateLiveText();
        StartCoroutine(WaitAndRestart(restartTime));
    }

    private IEnumerator WaitAndRestart(float restartTime)
    {
        yield return new WaitForSeconds(restartTime);
        Reset();

        if (lives > 0)
        {
            int indexScence = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(indexScence);
        }
        else
        {
            SceneManager.LoadScene(0);
            // Aqui se destruye el GameManager y AudioManager para tener uno nuevo
            Destroy(gameObject);
            Destroy(AudioManager.Instance.gameObject);
        }
    }

    public void IncreaseEnemiesLeft()
    {
        enemiesLeft++;
    }
    public void DecreaseEnemiesLeft()
    {
        enemiesLeft--;
        if (enemiesLeft == 0 && allWavesSpawned)
        {
            // Pasar de nivel
            NextScene();
        }
    }

    public void NextScene()
    {
        Reset();
        int indexNextScence = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(indexNextScence);
    }

    public void AllWavesSpawned()
    {
        allWavesSpawned = true;
        Debug.Log("Todos");
    }

    private void Reset()
    {
        enemiesLeft = 0;
        IsPlayerDead = false;
        allWavesSpawned = false;
    }
}
