using System;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] public Vector3 _offset = new Vector3(0,0,-20f);
    public float _SmoothTime = 0.25f;
    public Vector3 _velocity = Vector3.zero;
    public bool _canFollow = true;
    private GameObject _player;
    [SerializeField] float _yMin  ;
    [SerializeField] float _yMax = 15;
    [SerializeField] float _xMax;
    [SerializeField] float _xMin;
    [SerializeField] GameObject borderRight;
    [SerializeField] GameObject borderLeft;
    
    private float _duration = 3f;

    private void Start()
    {
       // _xMin = borderLeft.transform.position.x;
        //_xMax = borderRight.transform.position.x;
        _player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.position= new Vector3(_player.transform.position.x,_player.transform.position.y,-2.8f);
    }

    private void Update()
    {
        if (_canFollow)
        {
            float y = Mathf.Clamp(_player.transform.position.y, _yMin, _yMax);
            float x = Mathf.Clamp(_player.transform.position.x, _xMin, _xMax);
            Vector3 pos = new Vector3(x, y, _player.transform.position.z) + _offset;
            
            gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, pos,ref _velocity,_SmoothTime );
            
        }
            
        
    }

    void LateUpdate()
    {
        
        
       // float y = Mathf.Clamp(_player.transform.position.y, _yMin, _yMax);
        
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z); 
        
    }
}
