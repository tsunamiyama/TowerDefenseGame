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
        }

        upgradePanel.GetComponent<upgradePanel>().panelOff();
    }

    public void ballistaUpgrade(){
        //Upgrade template for future ballista
            if(buttonUpgradeType == "attackspeed"){
                ballistaTemplate.GetComponent<ballistaScript>().fireRate *= 1.1f;
            }
            if(buttonUpgradeType == "damage"){
                ballistaTemplate.GetComponent<ballistaScript>().damage *= 1.1f;
            }
            if(buttonUpgradeType == "range"){
                float x = ballistaTemplate.transform.GetChild(0).localScale.x;
                float z = ballistaTemplate.transform.GetChild(0).localScale.z;
                ballistaTemplate.transform.GetChild(0).localScale += new Vector3(x*0.1f, 0.0f, z*0.1f);
            }

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
}
