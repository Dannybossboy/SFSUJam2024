using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    private float time = 60;
    private TimeManager timeManager;
    private UIManager uiManager;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        timeManager = TimeManager.instance;
        uiManager = UIManager.Instance;
    }


    private void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
        } else if (time < 0)
        {
            time = 0;
            //Game Over
        }

        uiManager.SetTime(time);

    }
}
