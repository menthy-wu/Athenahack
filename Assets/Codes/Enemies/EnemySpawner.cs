using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameObject wave;
    List<GameObject> waves;
    int totalWaves;
    int currentwave = 0;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            waves.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
        Invoke("inableWave", 1f);
        totalWaves = transform.childCount;
    }

    private void FixedUpdate()
    {
        if (waves[currentwave].transform.childCount == 0)
        {
            waves[currentwave].SetActive(false);
            currentwave++;
            Invoke("inableWave", 1f);
        }
        if (currentwave == totalWaves)
            win();
    }

    void win()
    {
        Debug.Log("win");
    }

    void inableWave()
    {
        waves[currentwave].SetActive(true);
    }
}
