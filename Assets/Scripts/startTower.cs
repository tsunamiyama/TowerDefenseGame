using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startTower : MonoBehaviour
{
    public int health;
    public int money;
    public GameObject hpText;
    public GameObject moneyText;
    // Start is called before the first frame update
    void Start()
    {
        health = 1000;
        money = 100;
        hpText.GetComponent<TMPro.TextMeshProUGUI>().text = health.ToString();
        moneyText.GetComponent<TMPro.TextMeshProUGUI>().text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0){
            Destroy(this.gameObject);
        }
    }

    public void updateTowerHealth(int dmg){
        health = health - dmg;
        hpText.GetComponent<TMPro.TextMeshProUGUI>().text = health.ToString();
    }

    public void updateTowerMoney(int cost){
        money = money - cost;
        moneyText.GetComponent<TMPro.TextMeshProUGUI>().text = money.ToString();
    }

    public void addTowerMoney(int amount){
        money += amount;
        moneyText.GetComponent<TMPro.TextMeshProUGUI>().text = money.ToString();
    }
}
