using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    public TMP_Text rewindText;
    public TMP_Text timeText;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleRewindUI(bool rewinding)
    {
        if(rewinding)
        {
            rewindText.text = "<<";
        } else
        {
            rewindText.text = ">>";
        }

    }

    public void SetTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:00}: {1:00}", minutes, seconds);
    }
}
