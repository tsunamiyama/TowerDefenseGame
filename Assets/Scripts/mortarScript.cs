using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class mortarScript : MonoBehaviour
{
    public GameObject rangeIndicator;
    public GameObject parentBlock;
    public float fireRate;
    public float lastShot;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        rangeIndicator.GetComponent<MeshRenderer>().enabled = false;
        fireRate = 3.0f;
        damage = 1.0f;
        lastShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver(){
        if(!EventSystem.current.IsPointerOverGameObject()){
            if(parentBlock != null){
                parentBlock.GetComponent<gridCube>().OnMouseOver();
                rangeIndicator.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    void OnMouseExit(){
        if(!EventSystem.current.IsPointerOverGameObject()){
            rangeIndicator.GetComponent<MeshRenderer>().enabled = false;
            parentBlock.GetComponent<gridCube>().OnMouseExit();
        }
    }
}
