using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point{
    public int x,z;

}

public class TileManager : MonoBehaviour{
    public GameObject exampleTile;
    private HashSet<Point> tileList = new HashSet<Point>();

    public int length;

    public int width;

    public GameObject selectedBlock;

    private int chanceToFork = 0;

    private bool isForking = false;

    // Start is called before the first frame update
    void Start(){
        exampleTile.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void createTile(float newX, float newZ, Vector3 dir, int endRow, int endIndex, List<GameObject> currentPath){
        //Create new tile
        GameObject newTile = Instantiate(exampleTile, new Vector3(newX, 0.0f, newZ) + dir, transform.rotation);
        newTile.transform.parent = gameObject.transform;
        newTile.SetActive(true);

        //Add new tile to set of tile points
        Point newPoint;
        newPoint.x = (int)newTile.transform.localPosition.x;
        newPoint.z = (int)newTile.transform.localPosition.z;
        tileList.Add(newPoint);

        //pick random direction to expand to
        int newStart = 0;
        int newIndex = 0;
        int forkStart = 0;
        int forkIndex = 0;
        int tempStart = 0;
        int tempIndex = 0;
        int choice;
        int forkChoice = 0;
        bool validOption = false;


        //check if needs to fork
        if(Random.Range(0,100) <= chanceToFork){
            isForking = true;
            chanceToFork = 0;
        } else {
            chanceToFork += 0;
        }

        while(!validOption){
            choice = Random.Range(1,4);

            if(isForking && forkChoice != 0){
                while(choice == forkChoice){
                    choice = Random.Range(1,4);
                }
            }

            //Don't go up
            if(dir == new Vector3(-7.0f, 0.0f, 0.0f)){
                //go right
                if(choice == 1){
                    newStart = 0;
                    newIndex = Random.Range(1,5);
                    validOption = checkIfValidDirection(newPoint, "right");
                }
                //go down
                if(choice == 2){
                    newStart = Random.Range(1,5);
                    newIndex = 0;
                    validOption = checkIfValidDirection(newPoint, "down");
                }
                //go left
                if(choice == 3){
                    newStart = 6;
                    newIndex = Random.Range(1,5);
                    validOption = checkIfValidDirection(newPoint, "left");
                }
            }
            //Don't go right
            if(dir == new Vector3(0.0f, 0.0f, 7.0f)){
                //go left
                if(choice == 1){
                    newStart = 6;
                    newIndex = Random.Range(1,5);
                    validOption = checkIfValidDirection(newPoint, "left");
                }
                //go down
                if(choice == 2){
                    newStart = Random.Range(1,5);
                    newIndex = 0;
                    validOption = checkIfValidDirection(newPoint, "down");
                }
                //go up
                if(choice == 3){
                    newStart = Random.Range(1,5);
                    newIndex = 6;
                    validOption = checkIfValidDirection(newPoint, "up");
                }
            }
            //Don't go down
            if(dir == new Vector3(7.0f, 0.0f, 0.0f)){
                //go left
                if(choice == 1){
                    newStart = 6;
                    newIndex = Random.Range(1,5);
                    validOption = checkIfValidDirection(newPoint, "left");
                }
                //go right
                if(choice == 2){
                    newStart = 0;
                    newIndex = Random.Range(1,5);
                    validOption = checkIfValidDirection(newPoint, "right");
                }
                //go up
                if(choice == 3){
                    newStart = Random.Range(1,5);
                    newIndex = 6;
                    validOption = checkIfValidDirection(newPoint, "up");
                }
            }
            //Don't go left
            if(dir == new Vector3(0.0f, 0.0f, -7.0f)){
                //go down
                if(choice == 1){
                    newStart = Random.Range(1,5);
                    newIndex = 0;
                    validOption = checkIfValidDirection(newPoint, "down");
                }
                //go right
                if(choice == 2){
                    newStart = 0;
                    newIndex = Random.Range(1,5);
                    validOption = checkIfValidDirection(newPoint, "right");
                }
                //go up
                if(choice == 3){
                    newStart = Random.Range(1,5);
                    newIndex = 6;
                    validOption = checkIfValidDirection(newPoint, "up");
                }
            }
        
            if(!checkAllDirections(newPoint)){
                newStart = length/2;
                newIndex = width/2;
                break;
            }

            if(isForking && forkStart == 0 && forkIndex == 0){
                tempStart = newStart;
                tempIndex = newIndex;
                forkStart = 1;
                forkIndex = 1;
                forkChoice = choice;
                validOption = false;
            }
        }
        if(isForking){
            forkStart = newStart;
            forkIndex = newIndex;
            newStart = tempStart;
            newIndex = tempIndex;
            newTile.GetComponent<mapGrid>().populateMapWithFork(endRow, endIndex, newStart, newIndex, forkStart, forkIndex, validOption, currentPath);
            isForking = false;
        } else {
            newTile.GetComponent<mapGrid>().populateMap(endRow, endIndex, newStart, newIndex, validOption, currentPath);
        }
    }

    public bool checkIfValidDirection(Point tilePoint, string dir){
        Point nextPoint;

        switch(dir){
            case "up":
                nextPoint.x = tilePoint.x + 7;
                nextPoint.z = tilePoint.z;
                if(tileList.Contains(nextPoint)){
                    return false;
                }
                break;
            case "right":
                nextPoint.x = tilePoint.x;
                nextPoint.z = tilePoint.z - 7;
                if(tileList.Contains(nextPoint)){
                    return false;
                }
                break;
            case "down":
                nextPoint.x = tilePoint.x - 7;
                nextPoint.z = tilePoint.z;
                if(tileList.Contains(nextPoint)){
                    return false;
                }
                break;
            case "left":
                nextPoint.x = tilePoint.x;
                nextPoint.z = tilePoint.z + 7;
                if(tileList.Contains(nextPoint)){
                    return false;
                }
                break;
            default:
                return false;
        }

        return true;
    }

    public bool checkAllDirections(Point tilePoint){
        Point nextPoint;
        int count = 0;
        //Check Up
        nextPoint.x = tilePoint.x + 7;
        nextPoint.z = tilePoint.z;
        if(tileList.Contains(nextPoint)){
            count++;
        }

        //Check Right
        nextPoint.x = tilePoint.x;
        nextPoint.z = tilePoint.z - 7;
        if(tileList.Contains(nextPoint)){
            count++;
        }

        //Check Down
        nextPoint.x = tilePoint.x - 7;
        nextPoint.z = tilePoint.z;
        if(tileList.Contains(nextPoint)){
            count++;
        }

        //Check Left
        nextPoint.x = tilePoint.x;
        nextPoint.z = tilePoint.z + 7;
        if(tileList.Contains(nextPoint)){
            count++;
        }

        if(count != 4){
            return true;
        }

        return false;
    }
}
