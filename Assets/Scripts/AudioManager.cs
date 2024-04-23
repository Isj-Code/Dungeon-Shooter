using UnityEngine;
using TMPro;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource, musicAudioSource;
    [SerializeField] private TMP_Text musicText, sfxText;
    public static AudioManager Instance { get; private set; }

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
    }

    public void PlaySoundEffect(AudioClip audioClip, float volume)
    {
        sfxAudioSource.PlayOneShot(audioClip, volume);
    }

    public void ToggleSfx()
    {
        sfxAudioSource.mute = !sfxAudioSource.mute;
        if (sfxAudioSource.mute )
        {
            sfxText.text = "SFX: Off";
        }
        else
        {
            sfxText.text = "SFX: On";
        }
    }

    public void ToggleMusic()
    {
        musicAudioSource.mute = !musicAudioSource.mute;
        if (musicAudioSource.mute )
        {
            musicText.text = "Music: Off";
        }
        else
        {
            musicText.text = "Music: On";
        }
    }
}
