using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class AI_DistanceEnemy : MonoBehaviour
{

    private GameObject _target;
    private float _lastShot;

    [SerializeField] private int _ammo;
    [SerializeField] private float _fireRate;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _projectileSpawn;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _explosionDamage;
    [SerializeField] private float _detectionRange;
    [SerializeField] Quaternion  _targetRotationAdd;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
            float distance = Vector3.Distance(_target.transform.position, transform.position);
            if (distance < _detectionRange)
            {
                if (_ammo > 0)
                {
                    shoot();
                }
                else
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _target.transform.position, _dashSpeed * Time.deltaTime);
                }
                
                
                
            }
            else
            {
                
            }
            _projectileSpawn.transform.LookAt(_target.transform.position);
        
    }

    void shoot()
    {
        if (Time.time > _lastShot + _fireRate)
        {
            Vector3 relativePosition = _target.transform.position - transform.position; 
            Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.forward);
            Instantiate(_projectile, _projectileSpawn.transform.position, _projectileSpawn.transform.rotation);
            _lastShot = Time.time;
            _ammo--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && _ammo == 0)
        {
            collision.gameObject.GetComponent<PlayerStats>().looseHealth(_explosionDamage);
            Destroy(gameObject);
        }
    }
}
