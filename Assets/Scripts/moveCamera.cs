using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public float Speed = 0.000000000001f;
    Ray ray;
    RaycastHit hit;
 
     void Update()
     {
         //Move Camera
         float xAxisValue = Input.GetAxis("Horizontal")/4;
         float zAxisValue = Input.GetAxis("Vertical")/4;
 
         transform.position = new Vector3(transform.position.x + xAxisValue, transform.position.y, transform.position.z + zAxisValue);
         
         //Detect Clicks
         /*ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if(Physics.Raycast(ray, out hit))
         {
             Debug.Log(hit.collider.name);
         }*/
     }
}
