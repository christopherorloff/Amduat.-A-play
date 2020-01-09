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

        print("Buildindex: " + SceneManager.sceneCountInBuildSettings);

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
        //EventManager.snakeDeadEvent += StartChangeToNextScene;
        SceneManager.activeSceneChanged += ChangedActiveScene;

    }

    private void OnDisable()
    {
        //EventManager.snakeDeadEvent -= StartChangeToNextScene;
        SceneManager.activeSceneChanged -= ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene fromScene, Scene toScene)
    {
        print("ChangedActiveScene");

        EventManager.InvokeSceneChange();
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Cursor.visible = false;
        }
    }

    public void StartChangeToNextScene()
    {
        if (GetActiveSceneIndex() < SceneManager.sceneCountInBuildSettings - 1)
        {
            StartCoroutine(ChangeSceneCoroutine());
        }
        else
        {
            print("You are at the last scene");
        }
    }

    public int GetActiveSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    //Must be made generic when more scenes!
    internal IEnumerator ChangeSceneCoroutine()
    {
        print("Changing scene now biatch");
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        //Delay for fade or whatever
        yield return new WaitForSeconds(delayBeforeSceneChange);

        AsyncOperation nextSceneLoad = SceneManager.LoadSceneAsync(currentSceneBuildIndex + 1);

        while (!nextSceneLoad.isDone)
        {
            yield return null;
        }
        Scene next = SceneManager.GetSceneByBuildIndex(currentSceneBuildIndex + 1);
        SceneManager.SetActiveScene(next);
    }
}
