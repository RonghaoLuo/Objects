using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] menuBGMs, playBGMs;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject); // survives scene changes
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (audioSource.clip == clip && audioSource.isPlaying) return; // avoid restart
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayRandomMenuBGM()
    {
        AudioClip menuBGM = menuBGMs[Random.Range(0, menuBGMs.Length)];
        PlayMusic(menuBGM);
    }

    public void PlayRandomPlayBGM()
    {
        AudioClip playBGM = playBGMs[Random.Range(0, playBGMs.Length)];
        PlayMusic(playBGM);
    }
}
