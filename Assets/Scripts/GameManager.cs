using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
            // if the singleton hasn't been initialized yet
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;//Avoid doing anything else
            }

            Instance = this;
            DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        EventManager.snakeDeadEvent += StartToChangeScene;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= ChangedActiveScene;
        EventManager.snakeDeadEvent -= StartToChangeScene;

    }

    private void ChangedActiveScene(Scene fromScene, Scene toScene)
    {
        EventManager.sceneChange();
    }

    public void StartToChangeScene()
    {
        StartCoroutine(ChangeSceneCoroutine());
    }

    //Must be made generic when more scenes!
    internal IEnumerator ChangeSceneCoroutine()
    {
        yield return new WaitForSeconds(2);
        AsyncOperation nextSceneLoad = SceneManager.LoadSceneAsync("Hour7");
        print("after async load");
        //Delay for fade or whatever

        while (!nextSceneLoad.isDone)
        {
            print("Inside while");
            yield return null;
        }
        Scene next = SceneManager.GetSceneByName("Hour7");
        print("Outside while");
        SceneManager.SetActiveScene(next);
        print("After set active scene");
    }
}
