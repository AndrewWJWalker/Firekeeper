using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    //in minutes
    public float dayLength = 5;
    public float nightLength = 3;
    public Light sun;
    public Light moon;
    public Light campFire;
    public float campFireMin;
    public float campFireMax;
    public float campFireBlendTime;

    float sunIntensity;
    float moonIntensity;

    public EnemyAI enemyManager;
    public EnemySpawner enemySpawner;

    Transform sunTransform;
    Transform moonTransform;
    Quaternion sunRotationStored;
    Quaternion moonRotationStored;


    float daySeconds;
    float nightSeconds;
    float t = 0;

    public bool isDay = false;

    // Start is called before the first frame update
    void Start()
    {
        daySeconds = dayLength * 60;
        nightSeconds = nightLength * 60;
        sunTransform = sun.transform;
        moonTransform = moon.transform;
        sunRotationStored = sunTransform.rotation;
        moonRotationStored = moonTransform.rotation;
        sun.enabled = false;
        moon.enabled = true;
    }

    void FixedUpdate()
    {

        //tick
        t += Time.fixedDeltaTime;

        if (isDay)
        {
            //DAY
            if (t > daySeconds)
            {
                //switch to night
                isDay = false;
                t = 0;
                Sunset();
            } else
            {

                //get percentage of current day time
                float progress = t / daySeconds;
                //Debug.Log(progress);
                //reset sun rotation
                sunTransform.rotation = sunRotationStored;
                //rotate the sun
                sunTransform.rotation *= Quaternion.Euler( transform.right * (progress * 180));
                //set the sundial
                
            
                
            }



        }
        else
        {
            //NIGHT
            if (t > nightSeconds)
            {
                //switch to day
                isDay = true;
                Sunrise();
                t = 0;
            } else
            {
                //get percentage of current day time
                float progress = t / nightSeconds;
                //Debug.Log(progress);
                //reset sun rotation
                moonTransform.rotation = moonRotationStored;
                //rotate the sun
                moonTransform.rotation *= Quaternion.Euler(transform.right * (progress * 180));
                //set the sundial

            }
        }
        //Debug.Log("Time: " + t);

    }

    void Sunset()
    {
        sun.enabled = false;
        moon.enabled = true;

        //turn up the campfire
        StartCoroutine(TurnUpCampfire(campFireBlendTime));

        //start spawning enemies
        enemySpawner.BeginSpawn();
    }

    void Sunrise()
    {
        sun.enabled = true;
        moon.enabled = false;

        //turn down the campfire
        StartCoroutine(TurnDownCampfire(campFireBlendTime));

        //kill all enemies
        enemyManager.Exterminate();
    }

    IEnumerator TurnUpCampfire(float time)
    {
        float timePassed = 0;
        while (timePassed < time)
        {
            timePassed += Time.fixedDeltaTime;
            float factor = timePassed / time;
            float luminance = Mathf.Lerp(campFireMin, campFireMax, factor);
            campFire.intensity = luminance;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator TurnDownCampfire(float time)
    {
        float timePassed = 0;
        while (timePassed < time)
        {
            timePassed += Time.fixedDeltaTime;
            float factor = timePassed / time;
            float luminance = Mathf.Lerp(campFireMax, campFireMin, factor);
            campFire.intensity = luminance;
            yield return new WaitForFixedUpdate();
        }
    }
}
