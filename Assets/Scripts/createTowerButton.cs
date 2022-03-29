using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createTowerButton : MonoBehaviour
{
    public GameObject tileManager;
    public GameObject towerModel;
    public GameObject startTower;
    public GameObject boughtTowersParent;
    public string towerType;
    public int cost = 100;
    // Start is called before the first frame update
    void Start()
    {
        updateText();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void updateText(){
        gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =  towerType + ": $" + cost; 
    }

    public void selectTower(){
        if(tileManager.GetComponent<TileManager>().selectedBlock != null && startTower.GetComponent<startTower>().money >= cost && tileManager.GetComponent<TileManager>().selectedBlock.GetComponent<gridCube>().builtOn == false){
            var selectedBlock = tileManager.GetComponent<TileManager>().selectedBlock.gameObject;
            GameObject towerClone = Instantiate(towerModel);
            towerClone.transform.SetParent(boughtTowersParent.transform, true);
            towerClone.transform.position = selectedBlock.transform.position + new Vector3(0.0f, 0.25f, 0.0f);
            towerClone.transform.rotation = selectedBlock.transform.rotation;
            towerClone.SetActive(true);

            switch(towerType){
                case "ballista":
                    towerClone.GetComponent<ballistaScript>().parentBlock = selectedBlock;
                    break;
                case "mortar":
                    towerClone.GetComponent<mortarScript>().parentBlock = selectedBlock;
                    break;
            }

            startTower.GetComponent<startTower>().updateTowerMoney(cost);
            cost += (int)(cost*0.15f);
            updateText();

            tileManager.GetComponent<TileManager>().selectedBlock.GetComponent<gridCube>().builtOn = true;
        }
    }
}
