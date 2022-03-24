using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ballistaScript : MonoBehaviour
{
    public GameObject rangeIndicator;
    public float fireRate;
    public float lastShot;
    public GameObject arrow;
    public GameObject parentBlock;
    public GameObject towerTop;
    public GameObject towerBottom;
    // Start is called before the first frame update
    void Start()
    {
        rangeIndicator.GetComponent<MeshRenderer>().enabled = false;
        arrow.SetActive(false);
        fireRate = 1.75f;
        lastShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot(GameObject target){
        if(Time.time > fireRate + lastShot){
            //Debug.Log("Fire");
            GameObject arrowClone = Instantiate(arrow);
            arrowClone.transform.SetParent(arrow.transform.parent, false);
            arrowClone.transform.position = arrow.transform.position;
            arrowClone.transform.rotation = arrow.transform.rotation;
            arrowClone.GetComponent<arrowProjectileScript>().target = target;
            arrowClone.GetComponent<arrowProjectileScript>().launched = true;
            arrowClone.SetActive(true);
            
            lastShot = Time.time;
        }
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
