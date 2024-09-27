using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public Transform levelCamPos;
    private Camera cam;
    private Gamemanager gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        gamemanager = Gamemanager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(Move(cam.transform.position, levelCamPos.position, 1f));
            gamemanager.setCheckpointTime();
            loadNextLevel();
        }
    }

    IEnumerator Move(Vector3 start, Vector3 end, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            cam.transform.position = Vector3.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        cam.transform.position = end;
    }

    private void loadNextLevel()
    {

        switch(SceneManager.GetActiveScene().name)
        {
            case "Level1":
                SceneManager.LoadScene("Level2");
                break;
            case "Level2":
                SceneManager.LoadScene("Level3");
                break;
            case "Level3":
                SceneManager.LoadScene("End");
                break;
        }
    }
}
