using System;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
     [SerializeField] GameObject _cameraPosition;
    [SerializeField] GameObject _cameraObject;
    bool _triggered = false;
    public float _SmoothTime = 1f;
    public Vector3 _velocity = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        print(other.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            if(_cameraObject.GetComponent<CameraBehavior>()._canFollow == false)
            {
                _triggered = false;
                _cameraObject.GetComponent<CameraBehavior>()._canFollow = true;
                
            }
            else
            {
                _cameraObject.GetComponent<CameraBehavior>()._canFollow = false;
                //_cameraObject.transform.position = _cameraPosition.transform.position;
                _triggered = true;
            }
            
            
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (_triggered)
        {
            //ector3 pos = _player.transform.position + _offset;
            _cameraObject.transform.position = Vector3.SmoothDamp(_cameraObject.transform.position, _cameraPosition.transform.position,ref _velocity,_SmoothTime );
            
            
        }
    }
}
