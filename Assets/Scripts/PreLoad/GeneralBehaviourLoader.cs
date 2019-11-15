using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Deprecated. Just place the manager's prefab on the _app object!
///
/// Placed in the _preload scene,
/// this script instantiates general behaviours
/// at the beginning of the game.
/// </summary>
public class GeneralBehaviourLoader : MonoBehaviour
{
    public GameObject EventManagerPrefab;
    public GameObject GameManagerPrefab;
    public GameObject TileCoordinatesPrefab;
    
    //private void Awake()
    //{
    //    InitialiseComponent<EventManager>(EventManagerPrefab);
    //    InitialiseComponent<GameManager>(GameManagerPrefab);
    //    InitialiseComponent<TileCoordinates>(TileCoordinatesPrefab);
    //}

    /// <summary>
    /// Initializes a general behaviour script, that is stored in a prefab.
    /// The general behaviours must have a static Instance property.
    /// Checks if the Instance property has value and instantiates the prefab if it doesn't.
    /// </summary>
    //private void InitialiseComponent<T>(GameObject prefabObject) where T : MonoBehaviour
    //{
    //    Type componentType = typeof(T);

    //    PropertyInfo propertyInfo = componentType.GetProperty("Instance");

    //    if (propertyInfo == null)
    //    {
    //        Debug.LogError("Loader error: general behaviour of type " + componentType.Name +
    //                       " could not be initialised.");
    //        return;
    //    }

    //    var instance = propertyInfo.GetValue(null);

    //    if (instance != null) return;
    //    var newInstance = PrefabUtility.InstantiatePrefab(prefabObject);
    //    ((GameObject) newInstance).transform.parent = transform;
    //}
}