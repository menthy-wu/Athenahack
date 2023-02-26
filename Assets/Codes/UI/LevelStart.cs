using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour
{
    public int level = 1;

    public void press()
    {
        // Debug.Log(level);
        SceneManager.LoadScene(level + 1);
    }
}
