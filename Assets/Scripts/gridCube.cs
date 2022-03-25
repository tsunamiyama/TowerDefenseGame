using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;


public class gridCube : MonoBehaviour
{
    private bool walkable = true;
    private int gCost;
    private int hCost;
    private GameObject parent;
    public bool selected = false;
    public bool builtOn = false;
    private List<GameObject> neighbors = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        gCost = 0;
        hCost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(!walkable){
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }*/
    }

    public int getFCost(){
        return gCost + hCost;
    }

    public int getHCost(){
        return hCost;
    }

    public int getGCost(){
        return gCost;
    }

    public void setHCost(int i){
        hCost = i;
    }

    public void setGCost(int i){
        gCost = i;
    }

    public void addNeighbor(GameObject newNeighbor){
        neighbors.Add(newNeighbor);
    }

    public GameObject getNeighbor(int i){
        return neighbors[i];
    }

    public int numNeighbors(){
        return neighbors.Count;
    }

    public void setParent(GameObject p){
        parent = p;
    }

    public GameObject getParent(){
        return parent;
    }

    public bool getWalkable(){
        return walkable;
    }

    public void setWalkable(bool value){
        walkable = value;
    }

    public void OnMouseOver(){
        if(!EventSystem.current.IsPointerOverGameObject()){
            if(!selected){
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
            }
        }
    }

    public void OnMouseDown(){
        if(!EventSystem.current.IsPointerOverGameObject()){
            //deselect old block if there
            if(this.transform.parent.parent.parent.parent.gameObject.GetComponent<TileManager>().selectedBlock != null){
                this.transform.parent.parent.parent.parent.gameObject.GetComponent<TileManager>().selectedBlock.GetComponent<gridCube>().selected = false;
                this.transform.parent.parent.parent.parent.gameObject.GetComponent<TileManager>().selectedBlock.GetComponent<Renderer>().material.SetColor("_Color", new Color32(31,178,40,255));
            }
            //select this block now
            selected = true;
            this.transform.parent.parent.parent.parent.gameObject.GetComponent<TileManager>().selectedBlock = this.gameObject;
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
    }

    public void OnMouseExit(){
        if(!selected){
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color32(31,178,40,255));
        }
    }
}
