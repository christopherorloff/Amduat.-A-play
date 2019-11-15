using UnityEngine;

/// <summary>
/// DontDestroyOnLoad call for
/// the __app GameObject on the _preload
/// scene that contains all general behaviours
/// throughout the game.
/// </summary>
public class DDOL : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}