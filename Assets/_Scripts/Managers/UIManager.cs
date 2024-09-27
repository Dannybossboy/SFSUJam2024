using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public RectTransform watchHand;

    public Animator stopWatch;

    public Animator explosion;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (arg1.name == "End")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleRewindUI(bool rewinding)
    {
        /*
        if(rewinding)
        {
            rewindText.text = "<<";
        } else
        {
            rewindText.text = ">>";
        }
        */

        stopWatch.SetTrigger("Click");
    }

    public void SetTime(float time)
    {
        int seconds = Mathf.FloorToInt(time % 60);

        watchHand.localRotation = Quaternion.Euler(0,0, 360 * seconds / 60);
        
    }

    public void setExplosion()
    {
        explosion.SetTrigger("Dead");
    }
}
