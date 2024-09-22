using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    public TMP_Text rewindText;

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
}
