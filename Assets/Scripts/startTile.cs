using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startTile : MonoBehaviour
{
    public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        arrow.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(arrow){
            arrow.SetActive(true);
        }
    }
}
