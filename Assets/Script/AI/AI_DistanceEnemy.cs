using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class AI_DistanceEnemy : MonoBehaviour
{

    private GameObject _target;
    private float _lastShot;
    public Animator animator;
    private bool canDash = true;
    
    [SerializeField] private int _ammo;
    [SerializeField] private float _fireRate;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _projectileSpawn;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _explosionDamage;
    [SerializeField] private float _detectionRange;
    [SerializeField] Quaternion  _targetRotationAdd;
    private NavMeshAgent agent;

    public float animWait;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.magnitude > 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
            float distance = Vector3.Distance(_target.transform.position, transform.position);
            if (distance < _detectionRange)
            {
                if (_ammo > 0)
                {
                    StartCoroutine(shoot());
                }
                else
                {
                    agent.SetDestination(_target.transform.position);
                }
                
                
                
            }
            else
            {
                
            }
            _projectileSpawn.transform.LookAt(_target.transform.position);
        
    }
    
    IEnumerator shoot()
    {
        if (Time.time > _lastShot + _fireRate)
        {
            Vector3 relativePosition = _target.transform.position - transform.position; 
            Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.forward);
            gameObject.transform.rotation = rotation;
            _projectile.GetComponent<Projectile>().parent = this.gameObject;
            print("Throw");
            animator.SetTrigger("Throw");
            _lastShot = Time.time;
            _ammo--;
            yield return new WaitForSeconds(animWait);
            Instantiate(_projectile, _projectileSpawn.transform.position, _projectileSpawn.transform.rotation);
            
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

    void dashToPlayer()
    {
        if (canDash)
        {
            agent.SetDestination(_target.transform.position);
            canDash = false;
            Invoke("dashReset", 2f);
        }
        
        
    }

    void dashReset()
    {
        canDash = true;
        agent.ResetPath();
    }
}
