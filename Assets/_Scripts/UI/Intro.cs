using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    Coroutine routine;
    // Start is called before the first frame update
    void Start()
    {
        routine = StartCoroutine(play());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(routine);
            SceneManager.LoadScene("Cutscene");
        }
    }

    IEnumerator play()
    {
        yield return new WaitForSeconds(37);
        SceneManager.LoadScene("Cutscene");
    }
}
