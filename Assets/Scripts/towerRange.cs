using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerRange : MonoBehaviour
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
            turretTop.transform.LookAt(inRange[0].transform);
            tower.GetComponent<ballistaScript>().shoot(inRange[0]);
        }
    }

    private void OnTriggerEnter(Collider other){
        inRange.Add(other.gameObject);
        for(int i = 0; i < inRange.Count-1; i++){
            int min = i;
            for(int a = i + 1; a < inRange.Count; a++){
                if(Vector3.Distance(inRange[a].transform.position, startTower.transform.position) < Vector3.Distance(inRange[min].transform.position, startTower.transform.position)){
                    min = a;
                }
            }
            GameObject temp = inRange[i];
            inRange[i] = inRange[min];
            inRange[min] = temp;

        }
    }
}
