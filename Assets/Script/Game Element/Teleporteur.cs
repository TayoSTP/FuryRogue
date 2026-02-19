using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;

public class Teleporteur : MonoBehaviour
{
    public bool interact;
    public GameObject destination;
    GameObject UI;
    GameObject player;

    public bool canTeleport;

    public float fadeDuration = 1.5f;
    public float waitDuration = 1;
    public CanvasGroup canvasGroup;
    private Coroutine alphaChange;
    public GameObject newCamera;
    public GameObject previousCamera;
    List<Camera> cam;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //canvasGroup.alpha = 0;
        //FadeIn();
        cam = new List<Camera>().FindAll(x => x.gameObject.CompareTag("MainCamera"));
    }

    // Update is called once per frame
    void Update() 
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !interact)
        {
            player = other.gameObject;
            Teleport();
            
            
        }
        else if(other.CompareTag("Player") && interact)
        {
            //canTeleport = true;
          //  Instantiate(UI, transform.position + new Vector3(0,10,0), Quaternion.identity);
            player = other.gameObject;
            player.GetComponent<TestPlayerMovement>().teleporteur = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           canTeleport = false;
           previousCamera.SetActive(false); 
        }
        
        
    }

    public void Tp()
    {
        Teleport();
    }
    
     void Teleport()
    {
        print("Interact");
        player.SetActive(false);
        while (player.transform.position != destination.transform.position)
        {
            player.gameObject.transform.position = destination.transform.position;
        }
        player.SetActive(true);
        FadeIn();
        
    }

    void FadeIn()
    {
        canvasGroup.alpha = 1;
        FadeEffect(0);
    }
    void FadeEffect(float alpha)
    {
        if (alphaChange != null)
        {
            StopCoroutine(alphaChange);
        }

        alphaChange = StartCoroutine(AlphaChange(alpha));
    }
    
     IEnumerator AlphaChange(float alpha)
    {
        float timePassed = 0;
        float startAlpha = canvasGroup.alpha;
        cam.ForEach(x => x.gameObject.SetActive(false));
        newCamera.SetActive(true);
        yield return new WaitForSeconds(waitDuration);
        while (timePassed < fadeDuration)
        {
         timePassed +=  Time.deltaTime;
         
         canvasGroup.alpha = Mathf.Lerp(startAlpha, alpha, timePassed / fadeDuration);
         
         yield return null;
        }
        
        canvasGroup.alpha = alpha;

    }
}
