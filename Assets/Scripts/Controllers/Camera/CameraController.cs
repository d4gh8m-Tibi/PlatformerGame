using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float zoomSize = 5;

    public Transform target;
    public Vector3 Offset;
    public bool IsSmooth;
    [Range(1, 10)]
    public float SmoothFactor;



    private void Update()
    {
        ZoomLogic();
    }

    private void FixedUpdate()
    {
        FollowLogic();

    }

    private void FollowLogic()
    {
        Vector3 targetPosition = target.position + Offset;
        if (IsSmooth)
        {
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, SmoothFactor * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
        else
        {
            transform.position = targetPosition;
        }


    }

    private void ZoomLogic()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (zoomSize > 1)
            {
                zoomSize -= 1;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (zoomSize < 9)
            {
                zoomSize += 1;
            }
        }

        GetComponent<Camera>().orthographicSize = zoomSize;
    }
}
