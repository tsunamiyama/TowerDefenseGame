using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createBallistaButton : MonoBehaviour
{
    public GameObject tileManager;
    public GameObject ballistaModel;
    public GameObject startTower;
    public GameObject ballistaParent;
    public int cost = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void selectBallista(){
        if(tileManager.GetComponent<TileManager>().selectedBlock != null && startTower.GetComponent<startTower>().money >= cost && tileManager.GetComponent<TileManager>().selectedBlock.GetComponent<gridCube>().builtOn == false){
            var selectedBlock = tileManager.GetComponent<TileManager>().selectedBlock.gameObject;
            GameObject ballistaClone = Instantiate(ballistaModel);
            ballistaClone.transform.SetParent(ballistaParent.transform, true);
            ballistaClone.transform.position = selectedBlock.transform.position + new Vector3(0.0f, 0.25f, 0.0f);
            ballistaClone.transform.rotation = selectedBlock.transform.rotation;
            ballistaClone.SetActive(true);
            ballistaClone.GetComponent<ballistaScript>().parentBlock = selectedBlock;

            startTower.GetComponent<startTower>().updateTowerMoney(cost);

            tileManager.GetComponent<TileManager>().selectedBlock.GetComponent<gridCube>().builtOn = true;
        }
    }
}
