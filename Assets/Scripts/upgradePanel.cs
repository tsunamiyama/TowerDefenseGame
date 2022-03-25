using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeOption{
    public string upgradeText = "";
    public string upgradeTower = "";
    public string upgradeArea = "";
    public float upgradeValue = 0;

    public upgradeOption(string uText, string uTower, string uArea, float uValue){
        upgradeText = uText;
        upgradeTower = uTower;
        upgradeArea = uArea;
        upgradeValue = uValue;
    }
}

public class upgradePanel : MonoBehaviour
{
    public GameObject panel;
    public Camera cam;
    public GameObject upgradeButtonOne;
    public GameObject upgradeButtonTwo;
    public GameObject upgradeButtonThree;
    public GameObject ballistaParent;
    GameObject expandArrow;
    private List<upgradeOption> listofUpgrades = new List<upgradeOption>(){
        new upgradeOption(
            "Increase all ballista towers' range by 10%",
            "ballista",
            "range",
            10.0f
        ),
        new upgradeOption(
            "Increase all ballista towers' damage by 10%",
            "ballista",
            "damage",
            10.0f
        ),
        new upgradeOption(
            "Increase all ballista towers' attack speed by 10%",
            "ballista",
            "attackspeed",
            10.0f
        )
    };

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void panelOn(GameObject arrow){
        panel.SetActive(true);
        cam.GetComponent<dragCamera>().enabled = false;
        cam.GetComponent<moveCamera>().enabled = false;
        expandArrow = arrow;
        getUpgradeOptions(upgradeButtonOne);
        getUpgradeOptions(upgradeButtonTwo);
        getUpgradeOptions(upgradeButtonThree);
    }

    public void panelOff(){
        panel.SetActive(false);
        cam.GetComponent<dragCamera>().enabled = true;
        cam.GetComponent<moveCamera>().enabled = true;
        expandArrow.GetComponent<expandButton>().createNext();
    }

    public void getUpgradeOptions(GameObject upgrade){
        int chosenUpgrade = Random.Range(0, listofUpgrades.Count-1);
        upgrade.GetComponent<upgradeButton>().buttonUpgradeText = listofUpgrades[chosenUpgrade].upgradeText;
        upgrade.GetComponent<upgradeButton>().buttonUpgradeTower = listofUpgrades[chosenUpgrade].upgradeTower;
        upgrade.GetComponent<upgradeButton>().buttonUpgradeType = listofUpgrades[chosenUpgrade].upgradeArea;
        upgrade.GetComponent<upgradeButton>().buttonupgradeValue = listofUpgrades[chosenUpgrade].upgradeValue;

        upgrade.GetComponent<upgradeButton>().setButtonText();
    }
}
