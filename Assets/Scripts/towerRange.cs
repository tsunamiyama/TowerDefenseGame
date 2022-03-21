using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerRange : MonoBehaviour
{
    public List<GameObject> inRange = new List<GameObject>();
    public GameObject turretTop;
    public GameObject tower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
    }
}
