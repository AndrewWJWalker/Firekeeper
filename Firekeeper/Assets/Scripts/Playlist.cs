using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playlist : MonoBehaviour
{
    public List<AudioClip> daySongs;
    public List<AudioClip> nightSongs;
    public DayNightCycle dayNightManager;
    AudioSource audio;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            if (dayNightManager.isDay)
            {
                PlayNext(daySongs);
            } else {
                PlayNext(nightSongs);
            }
        }
    }

    public void PlayDay()
    {
        PlayNext(daySongs);
    }

    public void PlayNight()
    {
        PlayNext(nightSongs);
    }

    void PlayNext(List<AudioClip> clips)
    {
        index++;
        if (index > clips.Count)
        {
            index = 0;
        }
        audio.clip = clips[index];
        audio.Play();
    }

}
