using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(play());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator play()
    {
        yield return new WaitForSeconds(37);
        SceneManager.LoadScene("Cutscene");
    }
}
