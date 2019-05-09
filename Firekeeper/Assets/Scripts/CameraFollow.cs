using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float invalidBound = 5f;
    public float campSnapDistance = 20f;

    Vector3 offset;
    Vector3 movementOffset;
    Vector3 currentOffset;
    Vector3 smoothDampVelocity;
    bool canFollow;
    Camera cam;
    Player player;

    float t;

    private void Start()
    {
        player = target.GetComponent<Player>();
        offset = this.transform.position - Vector3.zero;
        cam = Camera.main;
    }
    void LateUpdate()
    {
        //if (t < 1)
        //{
        //    t += Time.deltaTime;
        //} else
        //{
        //    t = 1;
        //}
        float distanceToCamp = Vector3.Distance(Vector3.zero, target.position);
        if (distanceToCamp < campSnapDistance)
        {
            transform.position = Vector3.zero + offset;
        }
        else
        {


            Vector2 targetScreenSpace = cam.WorldToScreenPoint(target.position);
            targetScreenSpace = cam.ScreenToViewportPoint(targetScreenSpace);
            float distanceToCenter = Vector2.Distance(new Vector2(0.5f, 0.5f), targetScreenSpace);
            //Debug.Log(distanceToCenter);
            if (distanceToCenter > invalidBound)
            {
                canFollow = true;
                //  t = 0;

                movementOffset = this.transform.position - target.transform.position;
                if (player.moving)
                {
                    movementOffset += Vector3.Normalize(player.transform.forward) * 8f;
                }
            }
            else
            {
                canFollow = false;
            }


            if (distanceToCenter > 1)
            {
                //reset
                movementOffset = offset;
                transform.position = target.transform.position + movementOffset;
            }

            if (canFollow)
            {
                // Debug.Log(t);
                //currentOffset = Vector3.Lerp(movementOffset, offset, t);
                //currentOffset = Vector3.SmoothDamp(movementOffset, offset, ref smoothDampVelocity, 3f);
                transform.position = target.transform.position + movementOffset;

            }
        }
    }
}
