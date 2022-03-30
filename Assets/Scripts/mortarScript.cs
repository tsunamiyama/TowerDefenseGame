using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class mortarScript : MonoBehaviour
{
    public GameObject rangeIndicator;
    public GameObject parentBlock;
    public GameObject projectile;
    public float fireRate;
    public float lastShot;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        rangeIndicator.GetComponent<MeshRenderer>().enabled = false;
        projectile.SetActive(false);
        fireRate = 3.0f;
        damage = 1.0f;
        lastShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot(GameObject target){
        if(Time.time > fireRate + lastShot){
            //Debug.Log("Fire");
            GameObject projClone = Instantiate(projectile);
            //projClone.GetComponent<arrowProjectileScript>().damage = this.damage;
            projClone.transform.SetParent(projectile.transform.parent, false);
            projClone.transform.position = projectile.transform.position;
            projClone.transform.rotation = projectile.transform.rotation;
            //projClone.GetComponent<arrowProjectileScript>().target = target;
            //projClone.GetComponent<arrowProjectileScript>().launched = true;
            projClone.SetActive(true);
            projClone.GetComponent<Rigidbody>().AddForce(0,15.0f,0,ForceMode.Impulse);
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
