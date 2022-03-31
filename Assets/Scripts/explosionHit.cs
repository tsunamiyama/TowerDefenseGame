using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionHit : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Monster"){
            other.gameObject.GetComponent<monster>().health -= damage;
            if(other.gameObject.GetComponent<monster>().health <= 0){
                other.gameObject.GetComponent<monster>().die();
            }
        }
    }
}
