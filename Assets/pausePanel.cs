using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausePanel : MonoBehaviour
{
    public GameObject panel;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void panelOn(){
        panel.SetActive(true);
        cam.GetComponent<dragCamera>().enabled = false;
        cam.GetComponent<moveCamera>().enabled = false;
    }

    public void panelOff(){
        panel.SetActive(false);
        cam.GetComponent<dragCamera>().enabled = true;
        cam.GetComponent<moveCamera>().enabled = true;
    }
}
