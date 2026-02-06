using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, 0, target.position.z); 
    }
}
