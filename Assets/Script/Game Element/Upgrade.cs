using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public GameObject UI;

    public GameObject Arm;

    public UnityEngine.Camera cam;
    public GameObject cam2;
    private bool uiPos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (uiPos)
        {
            UI.transform.position =  RectTransformUtility.WorldToScreenPoint(cam, Arm.transform.position);
        }   
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>()._upgradeScript = this;
            UI.SetActive(true);
            uiPos = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>()._upgradeScript = null;
            UI.SetActive(false);
            uiPos = false;
        }
    }

    public void OpenMenu()
    {
        cam2.SetActive(true);
    }
}
