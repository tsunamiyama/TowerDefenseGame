using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragCamera : MonoBehaviour
{   
    public Camera mainCam;
    float MouseZoomSpeed = 15.0f;
    float TouchZoomSpeed = 0.1f;
    float ZoomMinBound = 10.0f;
    float ZoomMaxBound = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.touchSupported)
        {
            // Pinch to zoom
            if (Input.touchCount == 2)
            {
                // get current touch positions
                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);
                // get touch position from the previous frame
                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                float oldTouchDistance = Vector2.Distance (tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance (tZero.position, tOne.position);

                // get offset value
                float deltaDistance = oldTouchDistance - currentTouchDistance;
                Zoom (deltaDistance, TouchZoomSpeed);
            } else if(Input.touchCount == 1){
                Touch dragTouch = Input.GetTouch(0);
                float speed = 0.5f*Time.deltaTime;
                Vector3 inputDir = new Vector3(dragTouch.deltaPosition.x*speed, 0, dragTouch.deltaPosition.y*speed);
                inputDir = mainCam.transform.TransformDirection(inputDir);
                inputDir.y = 0;
                //inputDir.Normalize();
                mainCam.transform.position -= inputDir;
            }
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Zoom(scroll, MouseZoomSpeed);
        }

        if(mainCam.fieldOfView < ZoomMinBound) 
        {
            mainCam.fieldOfView = 0.1f;
        } else if(mainCam.fieldOfView > ZoomMaxBound) {
            mainCam.fieldOfView = 179.9f;
        }
    }

    void Zoom(float deltaMagnitudeDiff, float speed)
    {
        mainCam.fieldOfView += deltaMagnitudeDiff * speed;
        // set min and max value of Clamp function upon your requirement
        mainCam.fieldOfView = Mathf.Clamp(mainCam.fieldOfView, ZoomMinBound, ZoomMaxBound);
    }
}
