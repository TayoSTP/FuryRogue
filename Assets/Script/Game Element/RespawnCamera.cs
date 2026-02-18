using System;
using UnityEngine;
using System.Collections.Generic;
public class RespawnCamera : MonoBehaviour
{
    [SerializeField] GameObject lobbyCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            List<GameObject> cam = new List<GameObject>(GameObject.FindGameObjectsWithTag("MainCamera"));
            foreach (GameObject o in cam)
            {
                o.SetActive(false);
            }
            lobbyCamera.SetActive(true);
        }
    }
}
