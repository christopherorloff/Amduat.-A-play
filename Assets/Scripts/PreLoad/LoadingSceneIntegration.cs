using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handy helper script for the Editor,
/// makes sure that the first scene to
/// be loaded is the _preload scene.
/// </summary>
public class LoadingSceneIntegration : MonoBehaviour
{

#if UNITY_EDITOR 
    public static int loadedSceneIndex = -2;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitLoadingScene()
    {

        Application.targetFrameRate = 144; // Not sure we need this
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0) return;

        loadedSceneIndex = sceneIndex;
        // make sure the _preload scene is the first in the scene build list!
        SceneManager.LoadScene(0);
    }
#endif
}