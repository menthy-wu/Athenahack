using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSelection : MonoBehaviour
{
    [SerializeField]
    int level;

    [SerializeField]
    GameObject start;

    public void press()
    {
        start.GetComponent<LevelStart>().level = level;
    }
}
