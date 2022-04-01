using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowProjectileScript : MonoBehaviour
{
    public GameObject target;
    public GameObject tower;
    private Vector3 idlePosition;
    public bool launched = false;
    public bool damageDone = false;
    public float arrowSpeed = 12.0f;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        idlePosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(launched && target != null){
            if(damageDone == false){
                target.GetComponent<monster>().health -= (int)damage;
                damageDone = true;
            }
            gameObject.transform.LookAt(target.transform);
            this.transform.Translate(0,0,arrowSpeed*Time.deltaTime);

            if(Vector3.Distance(this.transform.position, target.transform.position) < 0.1f){
                //Debug.Log("hit");
                gameObject.transform.position = idlePosition;
                gameObject.SetActive(false);
                damageDone = false;
                if(target.GetComponent<monster>().health <= 0){
                    target.GetComponent<monster>().die();
                }
            }
        } else if(launched && target == null){
            gameObject.transform.position = idlePosition;
            gameObject.SetActive(false);
            tower.GetComponent<ballistaScript>().shoot(tower.GetComponent<ballistaScript>().rangeIndicator.GetComponent<movingTowerRange>().inRange[0]);
        }
    }
}
