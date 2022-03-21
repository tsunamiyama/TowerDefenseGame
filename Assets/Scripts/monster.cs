using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : MonoBehaviour
{
    public int currentWaypoint = 0;
    float speed = 1.0f;
    public int health;
    public int damage;
    public int value;
    public int monsterWaypointCategory;
    public GameObject monsterMan;
    public List<GameObject> listOfMonsterWaypoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start(){
        //Physics.IgnoreCollision(this.gameObject.GetComponent<BoxCollider>(), this.gameObject.GetComponent<BoxCollider>());
    }
    void Awake()
    {
        
    }

    public void getWaypoints(){
        int counter = 0;
        while(counter < monsterMan.GetComponent<monsterManager>().wayPoints.Count-1){
            int temp = monsterMan.GetComponent<monsterManager>().wayPoints[counter].GetComponent<waypoint>().waypointCategory;
            if(temp == monsterWaypointCategory || temp == 0){
                listOfMonsterWaypoints.Add(monsterMan.GetComponent<monsterManager>().wayPoints[counter]);
            }
            counter++;
        }
        currentWaypoint = listOfMonsterWaypoints.Count-1;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWaypoint < 0){
            monsterMan.GetComponent<monsterManager>().startTower.GetComponent<startTower>().updateTowerHealth(damage);
            Destroy(this.gameObject);
        }else {
            move();
        }
    }

    public void move(){
        this.transform.LookAt(listOfMonsterWaypoints[currentWaypoint].transform);
        this.transform.Translate(0,0,speed*Time.deltaTime);
        if(Vector3.Distance(this.transform.position, listOfMonsterWaypoints[currentWaypoint].transform.position) < 0.15f){
            currentWaypoint--;
        }
    }

    public void die(){
        monsterMan.GetComponent<monsterManager>().startTower.GetComponent<startTower>().addTowerMoney(value);
        Destroy(this.gameObject);
    }
}
