using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AI_CloseCombat : MonoBehaviour
{
    private GameObject _target;
    private float _lastHit;
    private float _currentHealth;
    private bool _rampage = false;
    private bool _canMove;
    private RaycastHit _hit;
    private float _distance =20;
    private AI_Stats _aiStats;
    private bool canDash = true;
    private bool moving;
    private Rigidbody rb;
    Animator anim;

    [SerializeField] private float _detectionRange;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _acceptanceRange;
    [SerializeField] private float _attackRate;
    [SerializeField] private float _hitDamage;
    [SerializeField] private float _maxHealth;
    
    public NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        _target = GameObject.FindGameObjectWithTag("Player");
        _currentHealth = _maxHealth;   
        _aiStats = GetComponent<AI_Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //gameObject.transform.LookAt(_target.transform.position);
        float distance = Vector3.Distance(_target.transform.position, transform.position);
        if (distance < _detectionRange)
        {
            gameObject.transform.rotation = Quaternion.LookRotation(_target.transform.position - transform.position);
        }
        if (distance < _detectionRange  && _canMove)
        {
            DashToPlayer();
            
            //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _target.transform.position, _dashSpeed * Time.deltaTime);
        }
        if (distance <= _acceptanceRange && !_rampage)
        {
            attack();
        }
        
        if (_aiStats._currentHealth <= (_aiStats._maxHealth*40)/100)
        {
            _rampage = true;
            _canMove = true;
        }

        if (!_canMove && distance > _detectionRange/3 )
        {
            _canMove = true;
        }
        if (agent.velocity.magnitude > 0f )
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && _rampage)
        {
            StartCoroutine(explode());
            
        }
        else if (collision.gameObject.CompareTag("Player") && !_rampage)
        {
            attack();
        }
    }

    IEnumerator explode()
    {
         yield return new WaitForSeconds(2f);
        var explo = Instantiate(_explosionPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void attack()
    {
        _canMove = false;
        if (_lastHit + _attackRate < Time.time)
        {
            anim.SetTrigger("Punch");
            _target.GetComponent<PlayerStats>().looseHealth(_hitDamage);
            _lastHit = Time.time;
        }
        
    }

    void DashToPlayer()
    {
        if (canDash)
        {
          agent.SetDestination(_target.transform.position);
          canDash = false;
          //Invoke("resetDash", 2);
        }
        
    }

    void resetDash()
    {
        canDash = true;
    }

    private void FixedUpdate()
    {
        anim.SetBool("Running", moving);
        
        Physics.Raycast(transform.position + new Vector3(0,1,0), transform.forward, out _hit,_distance);
        if (_hit.collider != null)
        {
            _distance = Vector3.Distance(_hit.transform.position, transform.position);
        }
        else
        {
            _distance = _detectionRange;
        }
        
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(gameObject.transform.position + new Vector3(0,1,0),  gameObject.transform.forward * _detectionRange);
    }
}
