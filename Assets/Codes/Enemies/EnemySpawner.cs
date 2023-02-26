using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameObject wave;
    List<GameObject> waves = new List<GameObject>();
    int totalWaves;
    int currentwave = 0;
    bool is_win = false;
    GameObject winUI;
    GameObject progress;
    float prograsslength;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            waves.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
        totalWaves = transform.childCount;
        winUI = GameObject.Find("WinUI");
        winUI.SetActive(false);
        progress = GameObject.Find("Progress").transform.Find("bar").gameObject;
        prograsslength = progress.transform.localScale.x;
    }

    private void Start()
    {
        Invoke("inableWave", 1f);
    }

    private void FixedUpdate()
    {
        if (is_win)
            return;
        if (waves[currentwave].transform.childCount == 0)
        {
            waves[currentwave].SetActive(false);
            currentwave++;
            progress.transform.localScale = new Vector3(
                prograsslength * ((totalWaves - currentwave) / (float)totalWaves),
                progress.transform.localScale.y,
                progress.transform.localScale.z
            );
            if (currentwave >= totalWaves)
            {
                Invoke("win", 1f);
                is_win = true;
                return;
            }
            Invoke("inableWave", 1f);
        }
    }

    void win()
    {
        winUI.SetActive(true);
    }

    void inableWave()
    {
        waves[currentwave].SetActive(true);
    }
}
