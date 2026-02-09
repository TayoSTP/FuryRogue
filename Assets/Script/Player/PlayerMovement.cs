using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.Mathematics;
using UnityEngine.InputSystem;

public class TestPlayerMovement : MonoBehaviour
{
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityScale;
    //[SerializeField] private float _fallingGravityScale;
    [SerializeField] private float _dashForce;
    
    private float _currentGravityScale;
    private bool _canDash = true;
    private float _gravity;
    private bool _grounded;  
    private Rigidbody _rb;
    private float pRot;
    public bool _followCamera = true;
    Vector2 _movement;
    
    

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _currentGravityScale = _gravityScale;
    }

    void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (_grounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            //_rb.linearVelocity += new Vector3(0, _jumpForce, 0);
        }
    }

    void OnDash(InputValue value)
    {
        if(_canDash) StartCoroutine("dash");
    }
    private void Update()
    {
        
        //Grounded Check
        
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.8f))
        { 
            _grounded = true;   
        }
        else
        {
            _grounded = false;
        }
        
    }
    
    IEnumerator dash()
    {
        _canDash = false;
        _rb.AddForce(new Vector3(_movement.x,0,0) *  _dashForce , ForceMode.Impulse);
        yield return new WaitForSeconds(0.2f);
        _canDash = true;
    }
    
    private void FixedUpdate()
    {
        //Vector3 movement = new Vector3(_movement.x, _rb.linearVelocity.y, 0);
        //if (movement  != Vector3.zero)
        //{
            _rb.AddForce(new Vector3(_movement.x,0,0) * _acceleration, ForceMode.Force);
            if (_rb.linearVelocity.magnitude > _maxSpeed)
            {
                _rb.linearVelocity = new Vector3((_rb.linearVelocity.normalized.x * _maxSpeed), _rb.linearVelocity.y);
            } 
       // }
        //else
        //{
        //   _rb.AddForce(_movement * -_deceleration, ForceMode.Force);
       // }
        
       if (_movement.x > 0.8f )
       {
           pRot = 0;
       }
       else if (_movement.x == 0)
       {
           
       }
       else if(_movement.x < 0)
       {
           pRot = 180;
       }
       gameObject.transform.localRotation = Quaternion.Euler(0, pRot, 0);
        _rb.AddForce((Vector3.down * _gravityScale), ForceMode.Force);
        print(_movement.x);
    }
}
