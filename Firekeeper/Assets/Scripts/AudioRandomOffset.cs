﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomOffset : MonoBehaviour
{


    AudioSource audio;
    public float offsetMax;
    public float delayBetween = 4f;
    public float pitchRange = 0.4f;
    bool canPlay = true;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Stop();

        float pitch = 1 + Random.Range(-pitchRange, pitchRange);
        audio.pitch = pitch;

        float delay = Random.Range(0, offsetMax);
        StartCoroutine(Delay(delay));
    }


    IEnumerator Delay(float delay)
    {
        canPlay = false;
        yield return new WaitForSeconds(delay);
        audio.Play();
        canPlay = true;
    }

    private void Update()
    {
        if (!audio.isPlaying && canPlay)
        {
            float delay = Random.Range(0, delayBetween);
            StartCoroutine(Delay(delay));
        }
    }

}
