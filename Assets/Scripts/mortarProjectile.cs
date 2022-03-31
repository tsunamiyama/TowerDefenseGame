using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mortarProjectile : MonoBehaviour
{
    public bool launched = false;
    public GameObject target;
    public GameObject ps;
    public GameObject arrowModel;
    public float damage;
    public bool damageDone = false;
    public Vector3 targetPos;
    bool targetSet = false;
    bool explosionPlay = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(launched && gameObject.GetComponent<Rigidbody>().velocity.y < 0){
            if(!targetSet){
                if(target == null){
                    targetPos = new Vector3(0,0,0);
                }else {
                    setTarget(target);
                }
                targetSet = true;
            }
            gameObject.transform.LookAt(targetPos);
            this.transform.Translate(0,0,25.0f*Time.deltaTime);
        }
        if(Vector3.Distance(this.transform.position, targetPos) < 1.0f){
            //Debug.Log("hit");
            launched = false;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            arrowModel.GetComponent<MeshRenderer>().enabled = false;
            if(!explosionPlay){
                ps.GetComponent<ParticleSystem>().Play();
                explosionPlay = true;
            }
            if(ps == null){
                Destroy(gameObject);
            }
        }
    }

    public void setTarget(GameObject t){
        targetPos = t.transform.position;
    }
}
