using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragCamera : MonoBehaviour
{   
    public Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetMouseButton(0)){
            float speed = 5*Time.deltaTime;
            Vector3 inputDir = new Vector3(Input.GetAxis("Mouse X")*speed, 0, Input.GetAxis("Mouse Y")*speed);
            inputDir = mainCam.transform.TransformDirection(inputDir);
            inputDir.y = 0;
            //inputDir.Normalize();
            mainCam.transform.position -= inputDir;
        }
    }
}
