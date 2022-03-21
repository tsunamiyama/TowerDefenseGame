using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGrid : MonoBehaviour {
    public int length;
    public int width;
    public GameObject dirtCube;
    public GameObject grassCube;
    public GameObject monsterManager;
    public GameObject arrowPointer;
    public GameObject wayPointHolder;
    GameObject[][] mapArray;

    // Start is called before the first frame update
    void Start(){

    }

    void Awake(){
        //hide cube used as reference
        dirtCube.SetActive(false);
        grassCube.SetActive(false);

        mapArray = new GameObject[length][];
        for(int i = 0; i < mapArray.Length; i++){
            mapArray[i] = new GameObject[width];
        }
    }

    // Update is called once per frame
    void Update(){
    }

    public void populateMap(int startRow, int startIndex, int endRow, int endIndex, bool needArrow){
        //place grass layer
        GameObject grassLayer = new GameObject();
        grassLayer.transform.parent = gameObject.transform;
        grassLayer.transform.position = grassLayer.transform.parent.position;
        grassLayer.name = "grassLayer";

        for(int a = 0; a < length; a++){
            GameObject row = new GameObject();
            row.transform.parent = grassLayer.transform;
            row.name =  "Row" + a;
            row.transform.localPosition = new Vector3(0, 0, -3.0f + a);
            for(int b = 0; b < width; b++){
                GameObject clone = Instantiate(grassCube);
                clone.transform.parent = row.transform;
                clone.transform.localPosition = new Vector3(-3.0f + b, 0.5f, 0.0f);
                clone.transform.rotation = transform.rotation;
                clone.SetActive(true);
                //Set unwalkable on border squares
                if(a == 0 || a == length-1 || b == 0 || b == width-1){
                    clone.GetComponent<gridCube>().setWalkable(false);
                }
                mapArray[a][b] = clone;
            }
        }

        //Establish neighbors for each block (no diagonals)
        for(int a = 0; a < length; a++){
            for(int b = 0; b < width; b++){
                //top neighbor
                if(a-1 >= 0){
                    mapArray[a][b].GetComponent<gridCube>().addNeighbor(mapArray[a-1][b]);
                }
                //right neighbor
                if(b+1 < width){
                    mapArray[a][b].GetComponent<gridCube>().addNeighbor(mapArray[a][b+1]);
                }
                //bottom neighbor
                if(a+1 < length){
                    mapArray[a][b].GetComponent<gridCube>().addNeighbor(mapArray[a+1][b]);
                }
                //left neighbor
                if(b-1 >= 0){
                    mapArray[a][b].GetComponent<gridCube>().addNeighbor(mapArray[a][b-1]);
                }
            }
        }
        
        //Set Paths
        mapArray[startRow][startIndex].GetComponent<gridCube>().setWalkable(true);
        //createPath(mapArray[startRow][startIndex], mapArray[length/2][width/2]);
        mapArray[endRow][endIndex].GetComponent<gridCube>().setWalkable(true);
        createPath(mapArray[startRow][startIndex], mapArray[endRow][endIndex], wayPointHolder);
        //retracePath(mapArray[startRow][startIndex], mapArray[endRow][endIndex], wayPointHolder);

        //Add expansion arrow to exit point
        //bottom row arrow
        if(needArrow){
            if(endIndex == 0){
                Debug.Log("Bottom Row");
                arrowPointer.transform.localPosition = new Vector3(mapArray[endRow][endIndex].transform.localPosition.x-1, 0, mapArray[endRow][endIndex].transform.parent.gameObject.transform.localPosition.z);
                monsterManager.GetComponent<monsterManager>().nextArrow = arrowPointer;
                //monsterManager.transform.position = arrowPointer.transform.position + new Vector3(1.0f, 0.5f, 0.0f);
                GameObject newSpawnPoint = new GameObject();
                newSpawnPoint.name = "newSpawnPoint";
                newSpawnPoint.transform.position = arrowPointer.transform.position + new Vector3(1.0f, 0.5f, 0.0f);
                monsterManager.GetComponent<monsterManager>().spawnPoints.Add(newSpawnPoint);
                monsterManager.GetComponent<monsterManager>().StartRound();
                //arrowPointer.SetActive(true);
            }
            //top row
            if(endIndex == width-1){
                Debug.Log("Top Row");
                arrowPointer.transform.localPosition = new Vector3(mapArray[endRow][endIndex].transform.localPosition.x+1, 0, mapArray[endRow][endIndex].transform.parent.gameObject.transform.localPosition.z);
                arrowPointer.transform.Rotate(0.0f,180.0f,0.0f, Space.Self);
                monsterManager.GetComponent<monsterManager>().nextArrow = arrowPointer;
                //monsterManager.transform.position = arrowPointer.transform.position + new Vector3(-1.0f, 0.5f, 0.0f);
                GameObject newSpawnPoint = new GameObject();
                newSpawnPoint.name = "newSpawnPoint";
                newSpawnPoint.transform.position = arrowPointer.transform.position + new Vector3(-1.0f, 0.5f, 0.0f);
                monsterManager.GetComponent<monsterManager>().spawnPoints.Add(newSpawnPoint);
                monsterManager.GetComponent<monsterManager>().StartRound();
                //arrowPointer.SetActive(true);
            }
            //left column
            if(endRow == length-1){
                Debug.Log("Left Col");
                arrowPointer.transform.localPosition = new Vector3(mapArray[endRow][endIndex].transform.localPosition.x, 0, mapArray[endRow][endIndex].transform.parent.gameObject.transform.localPosition.z+1);
                arrowPointer.transform.Rotate(0.0f,90.0f,0.0f, Space.Self);
                monsterManager.GetComponent<monsterManager>().nextArrow = arrowPointer;
                //monsterManager.transform.position = arrowPointer.transform.position + new Vector3(0.0f, 0.5f, -1.0f);
                GameObject newSpawnPoint = new GameObject();
                newSpawnPoint.name = "newSpawnPoint";
                newSpawnPoint.transform.position = arrowPointer.transform.position + new Vector3(0.0f, 0.5f, -1.0f);
                monsterManager.GetComponent<monsterManager>().spawnPoints.Add(newSpawnPoint);
                monsterManager.GetComponent<monsterManager>().StartRound();
                //arrowPointer.SetActive(true);
            }
            //right column
            if(endRow == 0){
                Debug.Log("Right Col");
                arrowPointer.transform.localPosition = new Vector3(mapArray[endRow][endIndex].transform.localPosition.x, 0, mapArray[endRow][endIndex].transform.parent.gameObject.transform.localPosition.z-1);
                arrowPointer.transform.Rotate(0.0f,270.0f,0.0f, Space.Self);
                monsterManager.GetComponent<monsterManager>().nextArrow = arrowPointer;
                //monsterManager.transform.position = arrowPointer.transform.position + new Vector3(0.0f, 0.5f, 1.0f);
                GameObject newSpawnPoint = new GameObject();
                newSpawnPoint.name = "newSpawnPoint";
                newSpawnPoint.transform.position = arrowPointer.transform.position + new Vector3(0.0f, 0.5f, 1.0f);
                monsterManager.GetComponent<monsterManager>().spawnPoints.Add(newSpawnPoint);
                monsterManager.GetComponent<monsterManager>().StartRound();
                //arrowPointer.SetActive(true);
            }
        } else {
            Debug.Log("DeadEnd");
            Destroy(arrowPointer);
        }
    }

    public void populateMapWithFork(int startRow, int startIndex, int endRowOne, int endIndexOne, int endRowTwo, int endIndexTwo, bool needArrow){
        //place grass layer
        GameObject grassLayer = new GameObject();
        grassLayer.transform.parent = gameObject.transform;
        grassLayer.transform.position = grassLayer.transform.parent.position;
        grassLayer.name = "grassLayer";

        for(int a = 0; a < length; a++){
            GameObject row = new GameObject();
            row.transform.parent = grassLayer.transform;
            row.name =  "Row" + a;
            row.transform.localPosition = new Vector3(0, 0, -3.0f + a);
            for(int b = 0; b < width; b++){
                GameObject clone = Instantiate(grassCube);
                clone.transform.parent = row.transform;
                clone.transform.localPosition = new Vector3(-3.0f + b, 0.5f, 0.0f);
                clone.transform.rotation = transform.rotation;
                clone.SetActive(true);
                //Set unwalkable on border squares
                if(a == 0 || a == length-1 || b == 0 || b == width-1){
                    clone.GetComponent<gridCube>().setWalkable(false);
                }
                mapArray[a][b] = clone;
            }
        }
        
        //Establish neighbors for each block (no diagonals)
        for(int a = 0; a < length; a++){
            for(int b = 0; b < width; b++){
                //top neighbor
                if(a-1 >= 0){
                    mapArray[a][b].GetComponent<gridCube>().addNeighbor(mapArray[a-1][b]);
                }
                //right neighbor
                if(b+1 < width){
                    mapArray[a][b].GetComponent<gridCube>().addNeighbor(mapArray[a][b+1]);
                }
                //bottom neighbor
                if(a+1 < length){
                    mapArray[a][b].GetComponent<gridCube>().addNeighbor(mapArray[a+1][b]);
                }
                //left neighbor
                if(b-1 >= 0){
                    mapArray[a][b].GetComponent<gridCube>().addNeighbor(mapArray[a][b-1]);
                }
            }
        }
        
        //Duplicate Necessary Stuff for Second Exit
        GameObject arrowPointer2 = Instantiate(arrowPointer, arrowPointer.transform.localPosition, arrowPointer.transform.rotation);
        arrowPointer2.transform.SetParent(arrowPointer.transform.parent, false);
        arrowPointer2.name = "newExpandArrow";
        
        GameObject wayPointHolder2 = Instantiate(wayPointHolder, wayPointHolder.transform.localPosition, wayPointHolder.transform.rotation);
        
        //Set Paths
        //Set Points Walkable
        mapArray[startRow][startIndex].GetComponent<gridCube>().setWalkable(true);
        mapArray[length/2][width/2].GetComponent<gridCube>().setWalkable(true);
        mapArray[endRowOne][endIndexOne].GetComponent<gridCube>().setWalkable(true);
        mapArray[endRowTwo][endIndexTwo].GetComponent<gridCube>().setWalkable(true);
        //Create Paths
        createPath(mapArray[startRow][startIndex], mapArray[length/2][width/2], wayPointHolder);
        createPath(mapArray[startRow][startIndex], mapArray[length/2][width/2], wayPointHolder2);
        createPath(mapArray[length/2][width/2], mapArray[endRowOne][endIndexOne], wayPointHolder);
        createPath(mapArray[length/2][width/2], mapArray[endRowTwo][endIndexTwo], wayPointHolder2);
        wayPointHolder.GetComponent<waypointHolder>().removeDuplicates();
        wayPointHolder2.GetComponent<waypointHolder>().removeDuplicates();

        //Add expansion arrow to exit point One
        if(needArrow){
            if(endIndexOne == 0){
                arrowPointer.transform.localPosition = new Vector3(mapArray[endRowOne][endIndexOne].transform.localPosition.x-1, 0, mapArray[endRowOne][endIndexOne].transform.parent.gameObject.transform.localPosition.z);
                monsterManager.GetComponent<monsterManager>().nextArrow = arrowPointer;
                monsterManager.transform.position = arrowPointer.transform.position + new Vector3(1.0f, 0.5f, 0.0f);
                //monsterManager.GetComponent<monsterManager>().StartRound();
                //arrowPointer.SetActive(true);
            }
            //top row
            if(endIndexOne == width-1){
                arrowPointer.transform.localPosition = new Vector3(mapArray[endRowOne][endIndexOne].transform.localPosition.x+1, 0, mapArray[endRowOne][endIndexOne].transform.parent.gameObject.transform.localPosition.z);
                arrowPointer.transform.Rotate(0.0f,180.0f,0.0f, Space.Self);
                monsterManager.GetComponent<monsterManager>().nextArrow = arrowPointer;
                monsterManager.transform.position = arrowPointer.transform.position + new Vector3(-1.0f, 0.5f, 0.0f);
                //monsterManager.GetComponent<monsterManager>().StartRound();
                //arrowPointer.SetActive(true);
            }
            //left column
            if(endRowOne == length-1){
                arrowPointer.transform.localPosition = new Vector3(mapArray[endRowOne][endIndexOne].transform.localPosition.x, 0, mapArray[endRowOne][endIndexOne].transform.parent.gameObject.transform.localPosition.z+1);
                arrowPointer.transform.Rotate(0.0f,90.0f,0.0f, Space.Self);
                monsterManager.GetComponent<monsterManager>().nextArrow = arrowPointer;
                monsterManager.transform.position = arrowPointer.transform.position + new Vector3(0.0f, 0.5f, -1.0f);
                //monsterManager.GetComponent<monsterManager>().StartRound();
                //arrowPointer.SetActive(true);
            }
            //right column
            if(endRowOne == 0){
                arrowPointer.transform.localPosition = new Vector3(mapArray[endRowOne][endIndexOne].transform.localPosition.x, 0, mapArray[endRowOne][endIndexOne].transform.parent.gameObject.transform.localPosition.z-1);
                arrowPointer.transform.Rotate(0.0f,270.0f,0.0f, Space.Self);
                monsterManager.GetComponent<monsterManager>().nextArrow = arrowPointer;
                monsterManager.transform.position = arrowPointer.transform.position + new Vector3(0.0f, 0.5f, 1.0f);
                //monsterManager.GetComponent<monsterManager>().StartRound();
                //arrowPointer.SetActive(true);
            }
        } else {
            Destroy(arrowPointer);
        }

        //Add expansion arrow to exit point Two
        if(needArrow){
            if(endIndexTwo == 0){
                arrowPointer2.transform.localPosition = new Vector3(mapArray[endRowTwo][endIndexTwo].transform.localPosition.x-1, 0, mapArray[endRowTwo][endIndexTwo].transform.parent.gameObject.transform.localPosition.z);
                //monsterManager2.GetComponent<monsterManager>().nextArrow = arrowPointer2;
                //monsterManager2.transform.position = arrowPointer2.transform.position + new Vector3(1.0f, 0.5f, 0.0f);
                //monsterManager.GetComponent<monsterManager>().StartRound();
                //arrowPointer2.SetActive(true);
            }
            //top row
            if(endIndexTwo == width-1){
                arrowPointer2.transform.localPosition = new Vector3(mapArray[endRowTwo][endIndexTwo].transform.localPosition.x+1, 0, mapArray[endRowTwo][endIndexTwo].transform.parent.gameObject.transform.localPosition.z);
                arrowPointer2.transform.Rotate(0.0f,180.0f,0.0f, Space.Self);
                //monsterManager2.GetComponent<monsterManager>().nextArrow = arrowPointer2;
                //monsterManager2.transform.position = arrowPointer2.transform.position + new Vector3(-1.0f, 0.5f, 0.0f);
                //monsterManager.GetComponent<monsterManager>().StartRound();
                //arrowPointer2.SetActive(true);
            }
            //left column
            if(endRowTwo == length-1){
                arrowPointer2.transform.localPosition = new Vector3(mapArray[endRowTwo][endIndexTwo].transform.localPosition.x, 0, mapArray[endRowTwo][endIndexTwo].transform.parent.gameObject.transform.localPosition.z+1);
                arrowPointer2.transform.Rotate(0.0f,90.0f,0.0f, Space.Self);
                //monsterManager2.GetComponent<monsterManager>().nextArrow = arrowPointer2;
                //monsterManager2.transform.position = arrowPointer2.transform.position + new Vector3(0.0f, 0.5f, -1.0f);
                //monsterManager.GetComponent<monsterManager>().StartRound();
                //arrowPointer2.SetActive(true);
            }
            //right column
            if(endRowTwo == 0){
                arrowPointer2.transform.localPosition = new Vector3(mapArray[endRowTwo][endIndexTwo].transform.localPosition.x, 0, mapArray[endRowTwo][endIndexTwo].transform.parent.gameObject.transform.localPosition.z-1);
                arrowPointer2.transform.Rotate(0.0f,270.0f,0.0f, Space.Self);
                //monsterManager2.GetComponent<monsterManager>().nextArrow = arrowPointer2;
                //monsterManager2.transform.position = arrowPointer2.transform.position + new Vector3(0.0f, 0.5f, 1.0f);
                //monsterManager.GetComponent<monsterManager>().StartRound();
                //arrowPointer2.SetActive(true);
            }
        } else {
            Destroy(arrowPointer2);
        }

        monsterManager.GetComponent<monsterManager>().StartRound();
        //monsterManager2.GetComponent<monsterManager>().StartRound();
    }
    public void createPath(GameObject start, GameObject end, GameObject wpH){
        GameObject startPos = start;
        GameObject endPos = end;

        //A* pathfinding algorithm between designated points
        List<GameObject> openSet = new List<GameObject>();
        HashSet<GameObject> closedSet = new HashSet<GameObject>();
        openSet.Add(startPos);

        while(openSet.Count > 0){
            GameObject curr = openSet[0];

            for(int i = 1; i < openSet.Count; i++){
                if(openSet[i].GetComponent<gridCube>().getFCost() < curr.GetComponent<gridCube>().getFCost() || 
                   openSet[i].GetComponent<gridCube>().getFCost() == curr.GetComponent<gridCube>().getFCost() && 
                   openSet[i].GetComponent<gridCube>().getHCost() < curr.GetComponent<gridCube>().getHCost()){
                        curr = openSet[i];
                }
            }

            openSet.Remove(curr);
            closedSet.Add(curr);

            if(curr == endPos){
                retracePath(startPos, endPos, wpH);
                return;
            }

            //Iterating through neighbors
            int size = curr.GetComponent<gridCube>().numNeighbors();
            for(int i = 0; i < size; i++){
                GameObject neighbor = curr.GetComponent<gridCube>().getNeighbor(i);
                if(!neighbor.GetComponent<gridCube>().getWalkable() || closedSet.Contains(neighbor)){
                    continue;
                }
                
                int newMoveToNeighbor = curr.GetComponent<gridCube>().getGCost() + getDistance(curr, neighbor);
                if(newMoveToNeighbor < neighbor.GetComponent<gridCube>().getGCost() || !openSet.Contains(neighbor)){
                    neighbor.GetComponent<gridCube>().setGCost(newMoveToNeighbor);
                    neighbor.GetComponent<gridCube>().setHCost(getDistance(neighbor, endPos));
                    neighbor.GetComponent<gridCube>().setParent(curr);

                    if(!openSet.Contains(neighbor)){
                        openSet.Add(neighbor);
                    }
                }
            }
        }
        
    }

    public int getDistance(GameObject a, GameObject b){
        return (int)(Mathf.Abs(a.transform.position.x - b.transform.position.x) + Mathf.Abs(a.transform.position.z - b.transform.position.z));
    }

    public void retracePath(GameObject start, GameObject stop, GameObject wpH){
        List<GameObject> path = new List<GameObject>();
        GameObject curr = stop;

        while(curr != start){
            path.Add(curr);
            curr = curr.GetComponent<gridCube>().getParent();
        }
        path.Add(curr);
        path.Reverse();

        foreach(GameObject cube in path){
            GameObject waypointClone = Instantiate(wayPointHolder.GetComponent<waypointHolder>().waypoint, cube.transform.position, cube.transform.rotation);
            waypointClone.transform.parent = wpH.transform;
            wpH.GetComponent<waypointHolder>().waypoints.Add(waypointClone);
            cube.SetActive(false);
            //Destroy(cube);
        }
        
        //wpH.GetComponent<waypointHolder>().removeDuplicates();
    }

}
