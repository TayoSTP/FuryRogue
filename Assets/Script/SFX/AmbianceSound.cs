using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public Collider Area;                       // The area of the sound
    public GameObject Player;                   // The object to track

    void Update()
    {
        //Locate closest point on the collider to the player 
        Vector3 closestPoint = Area.ClosestPoint(Player.transform.position);
        // Set position to closet point to the Player
        transform.position = closestPoint;
        
    }

}
