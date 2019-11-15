using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Once preload is done, return to the scene
/// that should be active - the loaded scene
/// in the Editor, or the first scene in a build.
/// </summary>
public class ReturnToScene : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_EDITOR

        if (LoadingSceneIntegration.loadedSceneIndex > 0)
        {
            SceneManager.LoadScene(LoadingSceneIntegration.loadedSceneIndex);
        }

#elif UNITY_STANDALONE
        SceneManager.LoadSceneAsync(1);
#endif
    }

}