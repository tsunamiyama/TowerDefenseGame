using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expandButton : MonoBehaviour
{
    public GameObject tileManager;
    public GameObject waypointManager;
    public GameObject monsterManager;
    public List<GameObject> currPath = new List<GameObject>();
    private Vector3 positionOne;
    private Vector3 positionTwo;
    private Vector3 positionThree;
    private Vector3 positionFour;

    // Start is called before the first frame update
    void Start()
    {
        positionOne = gameObject.transform.position + new Vector3(1.0f, 0.5f, 0.0f);
        positionTwo = gameObject.transform.position + new Vector3(-1.0f, 0.5f, 0.0f);
        positionThree = gameObject.transform.position + new Vector3(0.0f, 0.5f, -1.0f);
        positionFour = gameObject.transform.position + new Vector3(0.0f, 0.5f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown(){
        for(int i = 0; i < monsterManager.GetComponent<monsterManager>().spawnPoints.Count; i++){
            if(monsterManager.GetComponent<monsterManager>().spawnPoints[i].transform.position == positionOne ||
               monsterManager.GetComponent<monsterManager>().spawnPoints[i].transform.position == positionTwo ||
               monsterManager.GetComponent<monsterManager>().spawnPoints[i].transform.position == positionThree ||
               monsterManager.GetComponent<monsterManager>().spawnPoints[i].transform.position == positionFour){
                   Destroy(monsterManager.GetComponent<monsterManager>().spawnPoints[i]);
                   monsterManager.GetComponent<monsterManager>().spawnPoints.RemoveAt(i);
            }
        }

        monsterManager.GetComponent<monsterManager>().arrowList.Remove(gameObject);

        monsterManager.GetComponent<monsterManager>().roundNumber++;

        gameObject.SetActive(false);
        //go down
        if(gameObject.transform.localPosition.x < -3){
            //Debug.Log("Go Down");
            tileManager.GetComponent<TileManager>().createTile(gameObject.transform.parent.localPosition.x, gameObject.transform.parent.localPosition.z, new Vector3(-7.0f, 0.0f, 0.0f), (int)gameObject.transform.localPosition.z+3, 6, currPath);
        }
        //go left
        if(gameObject.transform.localPosition.z > 3){
            //Debug.Log("Go left");
            tileManager.GetComponent<TileManager>().createTile(gameObject.transform.parent.localPosition.x, gameObject.transform.parent.localPosition.z, new Vector3(0.0f, 0.0f, 7.0f), 0, (int)gameObject.transform.localPosition.x+3, currPath);
        }
        //go right
        if(gameObject.transform.localPosition.z < -3){
            //Debug.Log("Go right");
            tileManager.GetComponent<TileManager>().createTile(gameObject.transform.parent.localPosition.x, gameObject.transform.parent.localPosition.z, new Vector3(0.0f, 0.0f, -7.0f), 6, (int)gameObject.transform.localPosition.x+3, currPath);
        }
        //go up
        if(gameObject.transform.localPosition.x > 3){
            tileManager.GetComponent<TileManager>().createTile(gameObject.transform.parent.localPosition.x, gameObject.transform.parent.localPosition.z, new Vector3(7.0f, 0.0f, 0.0f), (int)gameObject.transform.localPosition.z+3, 0, currPath);
        }
        Destroy(gameObject);
    }
}
