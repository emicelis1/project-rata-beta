using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Header("Musica del Menu")]
    public AudioClip menuMusic;

    [Header("Musica por Nivel")]
    public AudioClip level1Music;
    public AudioClip level3Music;

    [Header("Volumen")]
    [Range(0f, 1f)]
    public float musicVolume = 0.6f;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = musicVolume;
        audioSource.loop = true;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        AudioClip clip = null;

        if (sceneName == "Menu" || sceneName == "Creditos")
        {
            clip = menuMusic;
        }
        else if (sceneName == "LEVEL1" || sceneName == "LEVEL2")
        {
            clip = level1Music;
        }
        else if (sceneName == "LEVEL3")
        {
            clip = level3Music;
        }

        if (clip != null)
        {
            audioSource.volume = musicVolume;

            if (audioSource.clip != clip)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}