using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    [SerializeField]
    int totalhealth = 5;
    List<GameObject> hearts = new List<GameObject>();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            hearts.Add(child.gameObject);
        }
    }

    public void damage()
    {
        totalhealth--;
        if (totalhealth < 0)
        {
            return;
        }
        hearts[totalhealth].SetActive(false);
    }
}
