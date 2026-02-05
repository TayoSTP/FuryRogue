using System;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    private Vector3 _newPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "RoomDoorTrigger")
        {
            _newPosition = other.GetComponent<NewCameraPointHolder>()._newCameraPoint.transform.position;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_newPosition != null)
        {
            gameObject.transform.position = new Vector3(Mathf.Lerp(gameObject.transform.position, _newPosition, Time.deltaTime);
        }
    }
}
