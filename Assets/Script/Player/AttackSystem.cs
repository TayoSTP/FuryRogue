using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class AttackSystem : MonoBehaviour
{
    
    private float _lastShot;
    private int _hitsnumber;
    private Vector2 _mousePos;
    private Vector2[] _segments;
    private LineRenderer _lineRenderer;
    private const float TIME_CURVE_ADDITION = 0.5f;
    private bool canAttack = true;
    private int attackNumber = 1;
    private float attackRate = 0.5f;
    private float lastAttack;
    bool arrowInHand = false;
    
       
    
    private int _ammo;
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private GameObject _muzzle;
    [SerializeField] private float _fireRate;
    [SerializeField] public float _attackDamage;
    [SerializeField] private int _segmentCount = 50;
    [SerializeField] private float _curveLength = 3.5f;
    [SerializeField] private float  _projectileGravityFromRB;
    [SerializeField] private GameObject _exploPrefab;
    [SerializeField] public int bigAttackChancePourcentage;
    [SerializeField] Animator _animator;
    public bool plugIn1;

    private PlayerControls controls;
    private GameObject arrow;
    public GameObject Crossbow;


    

    void Start()
    {
        _ammo = gameObject.GetComponent<PlayerStats>()._ammo;
        
        _animator.SetInteger(("AttackNumber"), attackNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (arrowInHand)
        {
            Crossbow.SetActive(true);
        }
        else
        {
            Crossbow.SetActive(false);
        }
       /* Vector2 startPos = _muzzle.transform.position;
        _segments[0] = startPos;
        _lineRenderer.SetPosition(0, startPos);

        Vector2 startVelocity = transform.forward * 650;
        for (int i = 1; i < _segmentCount; i++)
        {
            float timeOffset = (i *Time.fixedDeltaTime * _curveLength);
            
            Vector2 gravityOffset = TIME_CURVE_ADDITION * Physics.gravity * _projectileGravityFromRB * Mathf.Pow(timeOffset, 2);
             _segments[i] = _segments[0] + startVelocity * timeOffset + gravityOffset;
             _lineRenderer.SetPosition(i, _segments[i]);
        }*/
    }

    void OnAbility(InputValue value)
    {
        if (arrowInHand)
        {
            _animator.SetBool("ArrowInHand", true);
            _animator.SetTrigger("EquipCrossbow");
        }

        if (Time.time > _lastShot + _fireRate && _ammo > 0 && arrowInHand)
            {
                _arrowPrefab.GetComponent<Projectile>().parent = this.gameObject;
                arrow =  Instantiate(_arrowPrefab, _muzzle.transform.position, _muzzle.transform.rotation);
                


                _lastShot = Time.time;
                _ammo--;
            }
        
        if(!arrowInHand)
        {
            _animator.SetTrigger("EquipCrossbow");
            arrowInHand = true;
        }
        
    }

    void OnLightAttack(InputValue value)
    {
        
        if (canAttack)
        {
            arrowInHand = false;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Vector3 castOrigin = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);
            if (Physics.Raycast(castOrigin, fwd, out RaycastHit hit, 1.2f))
            {
                
                if (hit.collider.gameObject.CompareTag("Ennemy"))
                {
                    if ((100 * Random.Range(0, 100))/100 <= bigAttackChancePourcentage && plugIn1)
                    {
                        Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                        rb.AddForce(gameObject.transform.forward * 250);
                        hit.collider.gameObject.GetComponent<AI_Stats>().looseHealth(_attackDamage+10);
                        hit.collider.gameObject.GetComponent<NavMeshAgent>().destination = hit.collider.gameObject.transform.position;
                        _animator.SetInteger(("AttackNumber"), 3);
                        _hitsnumber = 0;
                        Instantiate(_exploPrefab, hit.point, Quaternion.identity);
                        print("boom");
                    }
                    else
                    {
                        print("next");
                        if (attackNumber == 1 )
                        {
                            _animator.SetInteger(("AttackNumber"), attackNumber);
                            attackNumber = 2;
                        }
                        else if(attackNumber ==2 )
                        {
                            _animator.SetInteger(("AttackNumber"), attackNumber);
                            attackNumber = 1;
                        }
                        else
                        {
                            attackNumber = 1;
                            _animator.SetInteger(("AttackNumber"), attackNumber);
                            
                        }
                        _animator.SetTrigger("Punch");
                        Invoke("ResetAttack", attackRate);
                    }
                    
                    Rigidbody _rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                    _rb.AddForce(gameObject.transform.forward * 200);
                    hit.collider.gameObject.GetComponent<AI_Stats>().looseHealth(_attackDamage);
                    _hitsnumber++;
                    
                
                }
            }
            _animator.SetTrigger("Punch");
            Invoke("ResetAttack", attackRate);
        }
        
        
    }

    void ResetAttack()
    {
        canAttack = true;
    }
    private void OnDrawGizmos() 
    {
        Vector3 castOrigin = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(castOrigin,   fwd * 2);
    }
}
