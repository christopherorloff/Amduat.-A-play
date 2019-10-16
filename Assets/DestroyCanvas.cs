using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyCanvas : MonoBehaviour
{
    public void DestroyObject() {
        SceneManager.LoadScene("WebGL Sound Test");
    }
}
