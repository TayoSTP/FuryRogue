using System;
using UnityEngine;

public class FireCollider : MonoBehaviour
{
    PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().looseHealth(Time.deltaTime + 0.5f);
        }
        
    }
}
