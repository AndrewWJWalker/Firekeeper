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
    public float lightTransitionTime;
    public Light campFire;
    public float campFireMin;
    public float campFireMax;
    public float campFireBlendTime;
    public float sunriseIntensity = 10;
    public float sunriseFlashTime = 0.3f;

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
    float dayProgress;
    float nightProgress;

    public bool isDay = false;
    public RectTransform sundialHandle;

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
        enemySpawner.BeginSpawn();
        sunIntensity = sun.intensity;
        moonIntensity = moon.intensity;
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
                dayProgress = t / daySeconds;
                //Debug.Log(progress);
                //reset sun rotation
                sunTransform.rotation = sunRotationStored;
                //rotate the sun
                sunTransform.rotation *= Quaternion.Euler( transform.right * (dayProgress * 180));
                //set the dayProgress
                
                if (dayProgress > 0.8)
                {
                    StartCoroutine(TurnDownSun(lightTransitionTime));
                }
                
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
                nightProgress = t / nightSeconds;
                //Debug.Log(progress);
                //reset sun rotation
                moonTransform.rotation = moonRotationStored;
                //rotate the sun
                moonTransform.rotation *= Quaternion.Euler(transform.right * (nightProgress * 180));
                //set the sundial

            }
        }
        //Debug.Log("Time: " + t);


        //sundial
        //z = 0 is night start, rotate negativley to go counterclockwise
        float angle;
        if (isDay)
        {
             angle = (1-dayProgress) * 180;
        }
        else
        {
             angle = -nightProgress * 180;
        }
        Vector3 rot = sundialHandle.eulerAngles;
        rot.z = angle;
        sundialHandle.eulerAngles = rot;

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
        StartCoroutine(TurnUpSun(lightTransitionTime));
        moon.enabled = false;

        //turn down the campfire
        StartCoroutine(TurnDownCampfire(campFireBlendTime));


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

    IEnumerator TurnUpSun(float time)
    {
        float timePassed = 0;
        while (timePassed < time)
        {
            timePassed += Time.fixedDeltaTime;
            float factor = timePassed / time;
            float luminance = Mathf.Lerp(0, sunIntensity, factor);
            sun.intensity = luminance;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(SunFlash(sunriseFlashTime));
    }

    IEnumerator SunFlash(float time)
    {
        float timePassed = 0;
        float startIntensity = sun.intensity;
        while (timePassed < time)
        {
            timePassed += Time.fixedDeltaTime;
            float factor = timePassed / time;
            float luminance = Mathf.Lerp(startIntensity, sunriseIntensity, factor);
            sun.intensity = luminance;
            yield return new WaitForFixedUpdate();
        }
        timePassed = 0;
        enemyManager.Exterminate();
        while (timePassed < time)
        {
            timePassed += Time.fixedDeltaTime;
            float factor = timePassed / time;
            float luminance = Mathf.Lerp(sunriseIntensity, startIntensity, factor);
            sun.intensity = luminance;
            yield return new WaitForFixedUpdate();
        }
      
    }

    IEnumerator TurnDownSun(float time)
    {
        float timePassed = 0;
        while (timePassed < time)
        {
            timePassed += Time.fixedDeltaTime;
            float factor = timePassed / time;
            float luminance = Mathf.Lerp(sunIntensity, 0, factor);
            sun.intensity = luminance;
            yield return new WaitForFixedUpdate();
        }
    }

}
