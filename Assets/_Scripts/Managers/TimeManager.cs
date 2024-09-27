using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public bool isRewinding;
    private Gamemanager gamemanager;

    //Audio
    public AudioSource musicSource;
    public AudioClip normalMusic;
    public AudioClip reverseMusic;

    private float currentTime;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    private void Start()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;


        gamemanager = Gamemanager.instance;

        musicSource.clip = normalMusic;
        musicSource.Play();
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (arg1.name == "End")
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //Toggles reverse time / normal time
        if (Input.GetButtonDown("ReverseTime"))
        {
            isRewinding = !isRewinding;
            UIManager.Instance.toggleRewindUI(isRewinding);
        }

        currentTime = musicSource.time;

        if (isRewinding && musicSource.clip != reverseMusic)
        {
            musicSource.clip = reverseMusic;
            musicSource.Play();
            musicSource.time = currentTime;
        } else if(!isRewinding && musicSource.clip != normalMusic)
        {
            musicSource.clip = normalMusic;
            musicSource.Play();
            musicSource.time = currentTime;
        }
    }
}
