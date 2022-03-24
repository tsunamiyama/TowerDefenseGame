using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterManager : MonoBehaviour
{
    public List<int> categories = new List<int>();
    public List<GameObject> wayPoints = new List<GameObject>();
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> arrowList = new List<GameObject>();
    int monsterNumber;
    public GameObject monster;
    public GameObject waypointHolder;
    public GameObject tileManager;
    bool waitingForLevelEnd = false;
    public GameObject startTower;
    public float spawnTime = 1.0f;
    
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
            for(int i = 0; i < arrowList.Count; i++){
                arrowList[i].SetActive(true);
            }
            waitingForLevelEnd = false;
            monsterNumber = (int)(monsterNumber*2.5);
        }
    }

    public void StartRound(){
        for(int i = 0; i < arrowList.Count; i++){
            if(arrowList[i] == null){
                    arrowList.RemoveAt(i);
                }
            arrowList[i].SetActive(false);
        }
        wayPoints = waypointHolder.GetComponent<waypointHolder>().waypoints;
        StartCoroutine(spawnMonster());
    }

    IEnumerator spawnMonster(){
        int count = monsterNumber;
        while(count > 0){
            for(int i = 0; i < spawnPoints.Count; i++){
                monster.GetComponent<monster>().listOfMonsterWaypoints = arrowList[i].GetComponent<expandButton>().currPath;
                monster.GetComponent<monster>().currentWaypoint = monster.GetComponent<monster>().listOfMonsterWaypoints.Count-1;
                GameObject monsterClone = Instantiate(monster);
                monsterClone.SetActive(true);
                monsterClone.transform.parent = gameObject.transform;
                monsterClone.transform.rotation = monster.transform.rotation;
                monsterClone.transform.localPosition = spawnPoints[i].transform.localPosition;
                yield return new WaitForSeconds(spawnTime);
            }
            count--;
        }
        waitingForLevelEnd = true;
    }
}
