using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class mortarScript : MonoBehaviour
{
    public GameObject rangeIndicator;
    public GameObject parentBlock;
    public GameObject projectile;
    public GameObject particleSystem;
    public float fireRate;
    public float lastShot;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        rangeIndicator.GetComponent<MeshRenderer>().enabled = false;
        projectile.SetActive(false);
        fireRate = 4.5f;
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
            lastShot = Time.time;
            StartCoroutine(launchProjectiles(target));
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

    IEnumerator launchProjectiles(GameObject target){
        for(int i = 0; i < 5; i++){
            GameObject projClone = Instantiate(projectile);
            //projClone.GetComponent<arrowProjectileScript>().damage = this.damage;
            projClone.transform.SetParent(projectile.transform.parent, false);
            projClone.transform.position = projectile.transform.position;
            projClone.transform.localPosition = new Vector3(Random.Range(-2,2), projClone.transform.localPosition.y, Random.Range(-2,2));
            projClone.transform.rotation = projectile.transform.rotation;
            projClone.GetComponent<mortarProjectile>().target = target;
            projClone.SetActive(true);
            projClone.GetComponent<Rigidbody>().AddForce(0,25.0f,0,ForceMode.Impulse);
            particleSystem.GetComponent<ParticleSystem>().Play();
            projClone.GetComponent<mortarProjectile>().launched = true;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
