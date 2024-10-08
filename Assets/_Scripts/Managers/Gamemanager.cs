using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    public float time;
    private static float checkpointTime = 62;
    private TimeManager timeManager;
    private UIManager uiManager;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    private void Start()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

        timeManager = TimeManager.instance;
        uiManager = UIManager.Instance;
        time = checkpointTime;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if(arg1.name == "End")
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        // Play State
        if (time > 0)
        {
            // Adds or subtracts time dependin on whether the plaeyr is reversing
            if (timeManager.isRewinding)
            {
                time += Time.deltaTime;
            }
            else
            {
                time -= Time.deltaTime;
            }

        }
        // Game Over State
        else if (time < 0)
        {
            time = 0;
            StartCoroutine(death());
        }

        // Adjust clock to the current time
        uiManager.SetTime(time);

        // Stops reversing time once player reaches the starting time
        if (time >= checkpointTime)
        {
            timeManager.isRewinding = false;
        }

        if(Input.GetButtonDown("Restart"))
        {
            StartCoroutine(death());
        }

    }

    public void setCheckpointTime()
    {
        checkpointTime = time;
    }

    private IEnumerator death()
    {
        
        uiManager.setExplosion();
        yield return new WaitForSeconds(1);
        time = checkpointTime;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
