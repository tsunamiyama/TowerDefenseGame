using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeButton : MonoBehaviour
{
    public string buttonUpgradeText = "";
    public string buttonUpgradeTower = "";
    public string buttonUpgradeType = "";
    public float buttonupgradeValue = 0.0f;
    public GameObject textHolder;
    public GameObject ballistaParent;
    public GameObject ballistaTemplate;
    public GameObject upgradePanel;
    public GameObject monsterManager;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setButtonText(){
        textHolder.GetComponent<TMPro.TextMeshProUGUI>().text = buttonUpgradeText;
    }

    public void activateUpgrade(){
        switch(buttonUpgradeTower){
            case "ballista":
                ballistaUpgrade();
                break;
            case "monster":
                monsterUpgrade();
                break;
        }

        upgradePanel.GetComponent<upgradePanel>().panelOff();
    }

    public void ballistaUpgrade(){
        //Upgrade template for future ballista
        if(buttonUpgradeType == "attackspeed"){
                float baseSpeed = 1.75f;
                ballistaTemplate.GetComponent<ballistaScript>().fireRate -= baseSpeed*0.1f;
            }
        if(buttonUpgradeType == "damage"){
                ballistaTemplate.GetComponent<ballistaScript>().damage *= 1.1f;
            }
        if(buttonUpgradeType == "range"){
                float x = ballistaTemplate.transform.GetChild(0).localScale.x;
                float z = ballistaTemplate.transform.GetChild(0).localScale.z;
                ballistaTemplate.transform.GetChild(0).localScale += new Vector3(x*0.1f, 0.0f, z*0.1f);
            }

        //Upgrade Rest of Already Purchsed Towers
        for(int i = 0; i < ballistaParent.transform.childCount; i++){
            if(buttonUpgradeType == "attackspeed"){
                ballistaParent.transform.GetChild(i).GetComponent<ballistaScript>().fireRate *= 1.1f;
            }
            if(buttonUpgradeType == "damage"){
                ballistaParent.transform.GetChild(i).GetComponent<ballistaScript>().damage *= 1.1f;
            }
            if(buttonUpgradeType == "range"){
                float x = ballistaParent.transform.GetChild(i).GetChild(0).localScale.x;
                float z = ballistaParent.transform.GetChild(i).GetChild(0).localScale.z;
                ballistaParent.transform.GetChild(i).GetChild(0).localScale += new Vector3(x*0.1f, 0.0f, z*0.1f);
            }
        }


    }
    public void monsterUpgrade(){
        for(int i = 0; i < monsterManager.transform.childCount; i++){
            if(buttonUpgradeType == "gold"){
                //monsterManager.transform.GetChild(i).gameObject.SetActive(true);
                int val = monsterManager.transform.GetChild(i).gameObject.GetComponent<monster>().value;
                monsterManager.transform.GetChild(i).gameObject.GetComponent<monster>().value = val + (int)(val*buttonupgradeValue);
                //monsterManager.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
