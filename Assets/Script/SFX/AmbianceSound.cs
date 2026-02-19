using System;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public Collider Area;                       // The area of the sound
    public GameObject Player;                   // The object to track
    public AudioSource Audio;
    private bool canFollow;
    void Update()
    {
        /*if (canFollow)
        {
            //Locate closest point on the collider to the player 
                    Vector3 closestPoint = Area.ClosestPoint(Player.transform.position);
                    // Set position to closet point to the Player
                    Audio.transform.position = closestPoint;
                    
        }*/
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            Audio.Play();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Audio.Stop();
        }
    }
}
