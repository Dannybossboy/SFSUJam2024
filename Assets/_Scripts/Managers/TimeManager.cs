using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public bool isRewinding;
    private Gamemanager gamemanager;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        gamemanager = Gamemanager.instance;
    }

    private void Update()
    {
        //Toggles reverse time / normal time
        if (Input.GetButtonDown("ReverseTime"))
        {
            isRewinding = !isRewinding;
            UIManager.Instance.toggleRewindUI(isRewinding);
        }
    }

    //Called when objects reach their timeline beggining
    public void endTimeline()
    {
        isRewinding = false;
        UIManager.Instance.toggleRewindUI(isRewinding);
    }
}
