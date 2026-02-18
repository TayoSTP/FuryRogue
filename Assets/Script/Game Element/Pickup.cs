using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pickup : MonoBehaviour
{
    //public pickupType pickupType;
    PlayerStats playerStats;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();   
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 1, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerStats.scraps += 1;
            Destroy(gameObject);
        }
    }
}
/*public Enum pickupType;
{
    string rock; 
    string paper ;
    string cissors ;
}*/
