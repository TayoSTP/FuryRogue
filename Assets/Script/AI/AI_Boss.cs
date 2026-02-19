using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI_Boss : MonoBehaviour
{
    private GameObject _player;
    bool _fight = false;
    bool _seePlayer = false;
    private float _lastShot;
    private float _lastHit;
    private bool _chase = false;
    private bool canRange = true;
    
    public GameObject[] _spawnPoints;
    public GameObject _ennemy;
    
    [Header("Settings")]
    [SerializeField] private float _rangeAttackFireRate;
    [SerializeField] private float _dashRate;
    [SerializeField] private GameObject _projectileSpawnSocket;
    [SerializeField] private float _meleeAttackRate;
    [SerializeField] private float _bigAttackRate;
    [SerializeField] private GameObject _fireSpitPrefab;
    
    [Header("Detection")]
    [SerializeField] private float _SightRange;
    [SerializeField] private float _meleAttackRange;
    [SerializeField] private float _distanceAttackRange;
    
    private bool _PlayerInMeleRange;
    private bool _PlayerInSight;
    private bool _PlayerInDistanceRange;
    private bool _alreadyAttacked;
    private bool _alreadyDashed;
    private bool _alreadyBigAttack;
    
    public LayerMask _whatIsPlayer;

    public NavMeshAgent agent;
    Animator _animator;
    public GameObject fireCollider;
    public GameObject bossHead;
    
    float lastSpit;
    int fireAmount;
    private AI_Stats _AIStats;

    public GameObject lifeUI;
    public GameObject VictoryUI;
    public Image healthBar;
    
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        _AIStats = gameObject.GetComponent<AI_Stats>();
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = _AIStats._currentHealth / _AIStats._maxHealth;
        _PlayerInMeleRange = Physics.CheckSphere(transform.position, _meleAttackRange, _whatIsPlayer);
        _PlayerInSight = Physics.CheckSphere(transform.position, _SightRange, _whatIsPlayer);
        _PlayerInDistanceRange = Physics.CheckSphere(transform.position, _distanceAttackRange,_whatIsPlayer);
        
        //if(_PlayerInSight && !_PlayerInRange && _player.transform.position.y > transform.position.y + 2) AttackPlayer();
        
        if(_PlayerInSight && !_PlayerInMeleRange && !_PlayerInDistanceRange) ChasePlayer();
        if (_PlayerInSight && !_PlayerInMeleRange && _PlayerInDistanceRange) StartCoroutine(RangeAttack());
        if (_PlayerInSight && _PlayerInMeleRange && _PlayerInDistanceRange) AttackPlayer();

        if (_AIStats._currentHealth <= 0)
        {
            VictoryUI.SetActive(true);
        }
        if (_PlayerInSight)
        {
            //fireCollider.transform.LookAt(_player.transform.position);
            //_projectileSpawnSocket.transform.LookAt(_player.transform.position);
            lifeUI.SetActive(true);
            bossHead.transform.LookAt(_player.transform);
        }
        else
        {
            lifeUI.SetActive(false);
        }
        float health = _AIStats._currentHealth;
        if(health <= 50)
        {
            print(_AIStats._currentHealth);
            StartCoroutine(BigAttack());
        }
    }
    
    IEnumerator RangeAttack()
    {
        
        if (canRange)
        {
            fireCollider.gameObject.SetActive(true);
            gameObject.transform.LookAt(_player.transform);
            canRange = false;
            gameObject.transform.LookAt(_player.transform);
            _animator.SetTrigger("FireSpit");
            yield return new WaitForSeconds(0.4f);
            
            while (fireAmount < 20)
            {
                Instantiate(_fireSpitPrefab, _projectileSpawnSocket.transform.position, _projectileSpawnSocket.transform.rotation);
                lastSpit =  Time.time;
                fireAmount++;
                yield return new WaitForSeconds(0.1f);
            }

            while (fireAmount < 20)
            {
                fireCollider.SetActive(true);
            }
            if (fireAmount == 20)
            {
                fireCollider.SetActive(false);
            }
            
            agent.SetDestination(transform.position);
            fireAmount = 0;
            Invoke("ResetRange", _rangeAttackFireRate);
        }
    }

    void ResetRange()
    {
        canRange = true;
    }

    void ChasePlayer()
    {
        
         if (!_alreadyDashed)
        {
            _alreadyDashed = true;
            
            print("Chase");
            agent.SetDestination(_player.transform.position);
            Invoke(nameof(ResetDash), _dashRate);
        }
        
        
    }

    void ResetDash()
    {
        _alreadyDashed = false;
    }

    void AttackPlayer()
    {
        
        
        //transform.LookAt(_player.transform);
        if (!_alreadyAttacked)
        {
            
            agent.SetDestination(transform.position);
            _alreadyAttacked = true;
            
            
            print("Melee");
            Invoke(nameof(ResetAttack), _meleeAttackRate);
        }
        
    }

    void ResetAttack()
    {
        _alreadyAttacked = false;
    }

     IEnumerator BigAttack()
    {
        if (!_alreadyBigAttack)
        {
            _animator.SetTrigger("MinionCast");
            _alreadyBigAttack = true;
            print("MAAAD");
            
           foreach (var _spawnPoint in _spawnPoints)
           {
               int rand = UnityEngine.Random.Range(2, 10);
               for (int i = 0; i < rand; i++)
               {
                   
                   Instantiate(_ennemy, _spawnPoint.transform.position, Quaternion.identity);
                   yield return new WaitForSeconds(0.5f);
               }
               
               
               
               
           } 
           Invoke(nameof(ResetAttack), _bigAttackRate);
        }
        
    }

    void ResetBigAttack()
    {
        _alreadyAttacked = false;
    }
    

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _SightRange);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _meleAttackRange);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _distanceAttackRange);
        Gizmos.DrawFrustum(new Vector3(), 59f, 1000f, 3f, 16f/9f);
        
    }
}
