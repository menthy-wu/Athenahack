using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundeffects : MonoBehaviour
{
    AudioSource buttonClick;
    AudioSource buttonTrigger;
    AudioSource loss;
    AudioSource mobDie1;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        buttonClick = transform.Find("buttonClick").gameObject.GetComponent<AudioSource>();
        buttonTrigger = transform.Find("buttonTrigger").gameObject.GetComponent<AudioSource>();
        loss = transform.Find("loss").gameObject.GetComponent<AudioSource>();
        mobDie1 = transform.Find("mobDie1").gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() { }

    public void playMusic(string audioName)
    {
        if (audioName == "buttonClick")
            buttonClick.Play();
        else if (audioName == "buttonTrigger")
            buttonTrigger.Play();
        else if (audioName == "loss")
            loss.Play();
        else if (audioName == "mobDie1")
            mobDie1.Play();
    }
}
