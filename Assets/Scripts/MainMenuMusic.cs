using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMusic : MonoBehaviour
{
    public AudioClip mainMenuMusic;
    public AudioClip instructionsMusic;
    public AudioClip creditsMusic;

    private AudioSource audioSource;

    public static MainMenuMusic Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Play the main menu music by default
        if (audioSource != null)
        {
            audioSource.clip = mainMenuMusic;
            audioSource.Play();
        }
    }

    private void OnEnable()
    {
        // Listen for scene change events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unlisten for scene change events
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check which scene was loaded and switch music accordingly
        switch (scene.buildIndex)
        {
            case 0:
                if (audioSource != null)
                {
                    audioSource.clip = mainMenuMusic;
                }
                break;
            case 2:
                if (audioSource != null)
                {
                    audioSource.clip = instructionsMusic;
                }
                break;
            case 3:
                if (audioSource != null)
                {
                    audioSource.clip = creditsMusic;
                }
                break;
            default:
                // Stop playing music if a scene other than 0, 2 or 3 is loaded
                if (audioSource != null)
                {
                    audioSource.Stop();
                }
                // Destroy the MainMenuMusic object when scene 1 is loaded
                Destroy(gameObject);
                return;
        }

        // Start playing the new music
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}