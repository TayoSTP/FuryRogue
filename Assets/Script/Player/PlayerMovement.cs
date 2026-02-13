using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.PlayerLoop;

public class TestPlayerMovement : MonoBehaviour
{
    
    [Header("Run")]
    [SerializeField] private float _acceleration;
    [SerializeField] private float _accelAmount;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _decelAmount;
    [SerializeField] private float _maxSpeed;
    
    
    [Range(0.01f, 1)] [SerializeField] private float _accelInAir;
    [Range(0.01f, 1)] [SerializeField] private float _decelInAir;
    [SerializeField] private float _jumpForce;

    private bool _conserveMomentum;
    [SerializeField] private float _gravityScale;
    //[SerializeField] private float _fallingGravityScale;
    [SerializeField] public float _dashForce;
    
    private float _currentGravityScale;
    private bool _canDash = true;
    private float _gravity;
    private bool _grounded;  
    private Rigidbody _rb;
    private float pRot;
    public bool _followCamera = true;
    Vector2 _movement;

    private bool isJumping;
    bool isJumpFalling = false;

    private float targetSpeed;
    
    
    [SerializeField] Animator _animator;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _currentGravityScale = _gravityScale;
        //_animator = gameObject.GetComponent<Animator>();
    }

    private void OnValidate()
    {
        _accelAmount = (50 * _acceleration) / _maxSpeed;
        _decelAmount = (50 * _deceleration) / _maxSpeed;

        #region Variable Ranges
        _acceleration = Mathf.Clamp(_acceleration, 0.01f, _maxSpeed);
        _deceleration = Mathf.Clamp(_deceleration, 0.01f, _maxSpeed);
        #endregion
    }

    void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
        _animator.SetFloat("MovementValue", _movement.x);
    }

    void OnJump(InputValue value)
    {
        if (_grounded)
        {
            _animator.SetTrigger("Jump");
            float jumpForce = _jumpForce;
            if (_rb.linearVelocity.y < 0)
            {
                jumpForce -= _rb.linearVelocity.y;
            }
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            /*if (_movement != Vector2.zero)
            {
                _rb.AddForce(Vector3.forward * _movement * 6.25f, ForceMode.Impulse);   
            }*/
            
            //_rb.linearVelocity += new Vector3(0, _jumpForce, 0);
        }
    }

    void OnDash(InputValue value)
    {
        if(_canDash) StartCoroutine("dash");
    }
    private void Update()
    {
        targetSpeed = _movement.magnitude;
        //Grounded Check
        
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.1f))
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
        
        _animator.SetBool("IsWalking", _movement.x != 0);
        Run();
        if (_rb.linearVelocity.y < 0)
        {
            _rb.AddForce((Vector3.down * (_gravityScale + 1.5f)), ForceMode.Acceleration);
        }
        else
        {
            _rb.AddForce((Vector3.down * _gravityScale), ForceMode.Acceleration);
        }
        //movement
        /*float speedDif = targetSpeed - _rb.linearVelocity.x;
        float movement = speedDif * _acceleration;
        float accelRate;
        if (_grounded)
        {
            accelRate = (Mathf.Abs(targetSpeed) > 0.1f) ? _acceleration : _deceleration;
        }
        _animator.SetFloat("Movement", _movement.x);
            //_rb.AddForce(new Vector3(_movement.x,0,0) * _acceleration, ForceMode.Force);
            _rb.AddForce(Vector3.right * movement, ForceMode.Force);
            if (_rb.linearVelocity.magnitude > _maxSpeed)
            {
                _rb.linearVelocity = new Vector3((_rb.linearVelocity.normalized.x * _maxSpeed), _rb.linearVelocity.y);
            } 
       // }
        //else
        //{
        //   _rb.AddForce(_movement * -_deceleration, ForceMode.Force);
       // }
        */
       if (_movement.x > 0.8f )
       {
           pRot = 90;
       }
       else if (_movement.x == 0)
       {
           
       }
       else if(_movement.x < 0)
       {
           pRot = -90;
       }
       gameObject.transform.localRotation = Quaternion.Euler(0, pRot, 0);
       // _rb.AddForce((Vector3.down * _gravityScale), ForceMode.Force);
        
    }

    void Run()
    {
        float targetSpeed = _movement.x * _maxSpeed;

        #region Calculate AccelRate
        float accelRate;

        if (_grounded)
        {
            accelRate = (Mathf.Abs(targetSpeed) > 0.1f) ? _accelAmount : _decelAmount;
        }
        else
        {
           // accelRate = (Mathf.Abs(targetSpeed) > 0.1f) ? _accelAmount * _accelInAir : _decelAmount * _decelInAir;
           accelRate = (Mathf.Abs(targetSpeed) > 0.1f) ? _accelAmount : _decelAmount;
        }
        #endregion
        
        {
            
        }
        
        #region Conserve Momentum

        if (_conserveMomentum && Mathf.Abs(_rb.linearVelocity.x) > Mathf.Abs(targetSpeed) &&
            Mathf.Sign(_rb.linearVelocity.x) == Mathf.Sign(targetSpeed))
        {
            accelRate = 0;
        }
        #endregion
        
        float speedDif = targetSpeed - _rb.linearVelocity.x;
        
        float movement = speedDif * accelRate;
        
        _rb.AddForce(Vector3.right * movement, ForceMode.Force);
    }
}
