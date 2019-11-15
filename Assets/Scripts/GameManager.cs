using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ScrollManager;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private float delayBeforeSceneChange = 5;


    private void Awake()
    {
        //Disable mouse
        Cursor.visible = false;


        // if the singleton hasn't been initialized yet
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        EventManager.snakeDeadEvent += StartChangeToNextScene;
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void OnDisable()
    {
        EventManager.snakeDeadEvent -= StartChangeToNextScene;
        SceneManager.activeSceneChanged -= ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene fromScene, Scene toScene)
    {
        print("ChangedActiveScene");

        if (SceneManager.GetActiveScene().name != "LoadingScene")
        {
            EventManager.sceneChange();
        }
    }

    public void StartChangeToNextScene()
    {
        StartCoroutine(ChangeSceneCoroutine());
    }

    public int GetActiveSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    //Must be made generic when more scenes!
    internal IEnumerator ChangeSceneCoroutine()
    {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        //Delay for fade or whatever
        yield return new WaitForSeconds(delayBeforeSceneChange);

        AsyncOperation nextSceneLoad = SceneManager.LoadSceneAsync(currentSceneBuildIndex + 1, LoadSceneMode.Additive);

        while (!nextSceneLoad.isDone)
        {
            yield return null;
        }
        Scene next = SceneManager.GetSceneByBuildIndex(currentSceneBuildIndex + 1);
        SceneManager.SetActiveScene(next);
    }
}
