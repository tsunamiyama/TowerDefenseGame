using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterManager : MonoBehaviour
{
    public List<List<GameObject>> listOfWaypoints = new List<List<GameObject>>();
    public List<GameObject> wayPoints = new List<GameObject>();

    public List<GameObject> spawnPoints = new List<GameObject>();

    int monsterNumber;

    public GameObject monster;

    public GameObject waypointHolder;

    public GameObject tileManager;

    bool waitingForLevelEnd = false;

    public GameObject nextArrow;

    public GameObject startTower;
    
    // Start is called before the first frame update
    void Start()
    {
        wayPoints = waypointHolder.GetComponent<waypointHolder>().waypoints;
        monster.SetActive(false);
        monsterNumber = 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(this.transform.childCount == 1 && waitingForLevelEnd){
            nextArrow.SetActive(true);
            waitingForLevelEnd = false;
            monsterNumber = (int)(monsterNumber*2.5);
        }
    }

    public void StartRound(){
        wayPoints = waypointHolder.GetComponent<waypointHolder>().waypoints;
        StartCoroutine(spawnMonster());
    }

    IEnumerator spawnMonster(){
        int count = monsterNumber;
        monster.GetComponent<monster>().monsterWaypointCategory = 1;
        monster.GetComponent<monster>().getWaypoints();
        while(count > 0){
            for(int i = 0; i < spawnPoints.Count; i++){
                if(spawnPoints.Count != 1){
                    monster.GetComponent<monster>().monsterWaypointCategory = i+1;
                    monster.GetComponent<monster>().getWaypoints();
                }
                GameObject monsterClone = Instantiate(monster);
                monsterClone.SetActive(true);
                monsterClone.transform.parent = gameObject.transform;
                monsterClone.transform.rotation = monster.transform.rotation;
                monsterClone.transform.localPosition = spawnPoints[i].transform.localPosition;
                yield return new WaitForSeconds(1.0f);
            }
            count--;
        }
        for(int i = 0; i < spawnPoints.Count; i++){
            Destroy(spawnPoints[i]);
        }
        spawnPoints.Clear();
        monster.GetComponent<monster>().listOfMonsterWaypoints.Clear();
        waitingForLevelEnd = true;
    }
}
