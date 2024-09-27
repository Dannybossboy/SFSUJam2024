using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject credits;
    public GameObject music;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ending());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ending()
    {
        yield return new WaitForSeconds(22);
        credits.SetActive(true);
        music.SetActive(true);

    }
}
