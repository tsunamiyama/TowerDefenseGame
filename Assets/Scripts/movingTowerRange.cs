using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingTowerRange : MonoBehaviour
{
    public List<GameObject> inRange = new List<GameObject>();
    public GameObject turretTop;
    public GameObject tower;
    public GameObject startTower;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(inRange.Count > 0 && (inRange[0] == null || inRange[0].GetComponent<monster>().health <= 0)){
            inRange.Remove(inRange[0]);
        } else if(inRange.Count > 0){
            inRange.Remove(null);
            turretTop.transform.LookAt(inRange[0].transform);
        }

    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Monster"){
            inRange.Add(other.gameObject);
        }
        for(int i = 0; i < inRange.Count-1; i++){
            if(inRange[i] == null){
                inRange.RemoveAt(i);
            }else {
                int min = i;
                for(int a = i + 1; a < inRange.Count; a++){
                    if(inRange[a] == null){
                        inRange.RemoveAt(a);
                    } else {
                        if(inRange[a].GetComponent<monster>().currentWaypoint < inRange[i].GetComponent<monster>().currentWaypoint){
                            min = a;
                        }
                    }
                }
                GameObject temp = inRange[i];
                inRange[i] = inRange[min];
                inRange[min] = temp;
            }
        }
    }

    private void OnTriggerExit(Collider other){
        inRange.Remove(other.gameObject);
    }
}
