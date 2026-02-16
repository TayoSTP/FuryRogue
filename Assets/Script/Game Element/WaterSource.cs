using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterSource : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Collider collider;
    private bool canInteract;
    GameObject player;
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnTriggerEnter(Collider collider)
    {
        print(collider.gameObject.name);
        if (collider.gameObject.CompareTag("Player"))
        {
            print("Player");
            canInteract = true;
            player = collider.gameObject;
        }
        
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = false;
            player = null;
        }
    }

    void OnInteract()
    {
        if (canInteract)
        {
            player.GetComponent<PlayerStats>().water = 100;
        }
    }
}
