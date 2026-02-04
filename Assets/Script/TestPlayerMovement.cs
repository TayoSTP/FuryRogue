using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    [SerializeField] private float PlayerSpeed;

    private void Update()
    {
        
      float moveHorizontal = Input.GetAxis("Horizontal");
      float moveVertical = Input.GetAxis("Vertical");  
        
        transform.position += new Vector3(moveHorizontal * PlayerSpeed, moveVertical * PlayerSpeed, 0);
        
    }

}
