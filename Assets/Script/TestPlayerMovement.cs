using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;

public class TestPlayerMovement : MonoBehaviour
{
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _fallingGravityScale;
    [SerializeField] private float _dashForce;
    
     private float _currentGravityScale;
    private bool _canDash = true;
    
    bool _grounded() {return Physics.Raycast(transform.position, Vector3.down, 1.1f);}
    
    private Rigidbody _rb;
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
        if (_grounded())
        {
            //_rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _rb.linearVelocity += new Vector3(0,_jumpForce) ;
        }
        
    }

    void OnDash(InputValue value)
    {
        if(_canDash) StartCoroutine("dash");
    }
    private void Update()
    {
        
        if (_rb.linearVelocity.y >= 0)
        {
            _currentGravityScale = _gravityScale;
        }
        else
        {
            _currentGravityScale = _fallingGravityScale;
        }
        
    }
    
    IEnumerator dash()
    {
        _canDash = false;
        _rb.AddForce(_movement *  _dashForce , ForceMode.Impulse);
        yield return new WaitForSeconds(0.2f);
        _canDash = true;
    }
    
    private void FixedUpdate()
    {
        
        _rb.AddForce(Physics.gravity *(_currentGravityScale - 1 ) * _rb.mass);
        
        
        
        if (_movement != Vector2.zero)
        {
            _rb.AddForce(_movement * _acceleration, ForceMode.Force);
            if (_rb.linearVelocity.magnitude > _maxSpeed)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * _maxSpeed;
            }
        }
        else
        {
            _rb.AddForce(_rb.linearVelocity * -_deceleration , ForceMode.Force);
        }
    }
}
