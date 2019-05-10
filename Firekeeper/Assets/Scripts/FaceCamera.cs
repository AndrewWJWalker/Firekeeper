using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{

    Camera cam;



    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform);
        Vector3 eulers = transform.eulerAngles;
        eulers.x = 0;
        eulers.z = 0;
        transform.eulerAngles = eulers;
    }
}
