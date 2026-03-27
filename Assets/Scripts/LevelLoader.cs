using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Canvas canvas;
    public Animator transition;

    // Update is called once per frame
    void Update()
    {
        //canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }

    public void LoadNextLevel(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}