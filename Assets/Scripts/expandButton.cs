using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expandButton : MonoBehaviour
{
    public GameObject tileManager;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown(){
        gameObject.SetActive(false);
        //go down
        if(gameObject.transform.localPosition.x < -3){
            //Debug.Log("Go Down");
            tileManager.GetComponent<TileManager>().createTile(gameObject.transform.parent.localPosition.x, gameObject.transform.parent.localPosition.z, new Vector3(-7.0f, 0.0f, 0.0f), (int)gameObject.transform.localPosition.z+3, 6);
        }
        //go left
        if(gameObject.transform.localPosition.z > 3){
            //Debug.Log("Go left");
            tileManager.GetComponent<TileManager>().createTile(gameObject.transform.parent.localPosition.x, gameObject.transform.parent.localPosition.z, new Vector3(0.0f, 0.0f, 7.0f), 0, (int)gameObject.transform.localPosition.x+3);
        }
        //go right
        if(gameObject.transform.localPosition.z < -3){
            //Debug.Log("Go right");
            tileManager.GetComponent<TileManager>().createTile(gameObject.transform.parent.localPosition.x, gameObject.transform.parent.localPosition.z, new Vector3(0.0f, 0.0f, -7.0f), 6, (int)gameObject.transform.localPosition.x+3);
        }
        //go up
        if(gameObject.transform.localPosition.x > 3){
            tileManager.GetComponent<TileManager>().createTile(gameObject.transform.parent.localPosition.x, gameObject.transform.parent.localPosition.z, new Vector3(7.0f, 0.0f, 0.0f), (int)gameObject.transform.localPosition.z+3, 0);
        }
        Destroy(gameObject);
    }
}
