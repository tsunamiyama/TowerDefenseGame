using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowProjectileScript : MonoBehaviour
{
    public bool launched = false;
    public GameObject target;
    public float arrowSpeed = 12.0f;
    public int damage = 1;
    public bool damageDone = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            Destroy(this.gameObject);
        }
        if(launched && target != null){
            if(damageDone == false){
                target.GetComponent<monster>().health -= damage;
                damageDone = true;
            }
            gameObject.transform.LookAt(target.transform);
            this.transform.Translate(0,0,arrowSpeed*Time.deltaTime);

            if(Vector3.Distance(this.transform.position, target.transform.position) < 0.1f){
                //Debug.Log("hit");
                Destroy(this.gameObject);
                if(target.GetComponent<monster>().health <= 0){
                    target.GetComponent<monster>().die();
                }
            }
        }
    }
}
