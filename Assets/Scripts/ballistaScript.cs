using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ballistaScript : MonoBehaviour
{
    public GameObject rangeIndicator;
    public float fireRate;
    public float lastShot;
    public float damage;
    public GameObject arrow;
    public GameObject parentBlock;
    public GameObject towerTop;
    public GameObject towerBottom;
    // Start is called before the first frame update
    void Start()
    {
        rangeIndicator.GetComponent<MeshRenderer>().enabled = false;

        fireRate = 1.75f;
        lastShot = Time.time;
        damage = 1.0f;

        arrow.SetActive(false);
        arrow.GetComponent<arrowProjectileScript>().damage = this.damage;
    }

    // Update is called once per frame
    void Update()
    {
        if(rangeIndicator.GetComponent<movingTowerRange>().inRange.Count > 0){
            if(Time.time > fireRate + lastShot){
                shoot(rangeIndicator.GetComponent<movingTowerRange>().inRange[0]);
            }
        }
    }

    public void shoot(GameObject target){
        //Debug.Log("Fire");
        arrow.SetActive(true);
        arrow.GetComponent<arrowProjectileScript>().target = target;
        arrow.GetComponent<arrowProjectileScript>().launched = true;
            
        lastShot = Time.time;
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
