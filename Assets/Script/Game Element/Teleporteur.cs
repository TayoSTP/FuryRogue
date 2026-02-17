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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //canvasGroup.alpha = 0;
        //FadeIn();
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !interact)
        {
            player = other.gameObject;
            other.transform.position = destination.transform.position;
            FadeIn();
        }
        else if(other.tag == "Player" && interact)
        {
            canTeleport = true;
            Instantiate(UI, transform.position + new Vector3(0,10,0), Quaternion.identity);
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canTeleport = false;
    }

    void OnInteract(InputValue value)
    {
        if (canTeleport)
        {
            player.transform.position = destination.transform.position;
        }
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
