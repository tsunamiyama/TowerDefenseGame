using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointHolder : MonoBehaviour
{
    public List<GameObject> waypoints = new List<GameObject>();
    public List<List<GameObject>> waypointPaths = new List<List<GameObject>>();
    public GameObject firstArrow;
    public GameObject waypoint;
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> presetPath = new List<GameObject>();
        for(int i = 0; i < gameObject.transform.childCount; i++){
            presetPath.Add(gameObject.transform.GetChild(i).gameObject);
        }
        waypointPaths.Add(presetPath);
        firstArrow.GetComponent<expandButton>().currPath = presetPath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removeDuplicates(){
        for(int i = 0; i < waypoints.Count-1; i++){
            for(int a = i+1; a < waypoints.Count; a++){
                if((waypoints[i].transform.position.x == waypoints[a].transform.position.x) && (waypoints[i].transform.position.z == waypoints[a].transform.position.z)){
                    Destroy(waypoints[a]);
                    waypoints.Remove(waypoints[a]);
                }
            }
        }
    }
}
