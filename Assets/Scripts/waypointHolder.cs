using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointHolder : MonoBehaviour
{
    public List<GameObject> waypoints = new List<GameObject>();

    public GameObject waypoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removeDuplicates(){
        for(int i = 0; i < waypoints.Count; i++){
            for(int a = 1; a < waypoints.Count; a++){
                if((waypoints[i].transform.position.x == waypoints[a].transform.position.x) && (waypoints[i].transform.position.z == waypoints[a].transform.position.z)){
                    Destroy(waypoints[a]);
                    waypoints.Remove(waypoints[a]);
                }
            }
        }
    }
}
