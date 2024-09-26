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
        timeManager = TimeManager.instance;
        uiManager = UIManager.Instance;
        time = checkpointTime;
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

        }

        // Adjust clock to the current time
        uiManager.SetTime(time);

        // Stops reversing time once player reaches the starting time
        if (time >= checkpointTime)
        {
            timeManager.isRewinding = false;
        }

    }

    public void setCheckpointTime()
    {
        checkpointTime = time + 1;
    }

    private IEnumerator death()
    {
        time = checkpointTime;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
