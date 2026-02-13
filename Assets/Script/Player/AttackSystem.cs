using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
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
    
    void Start()
    {
        _ammo = gameObject.GetComponent<PlayerStats>()._ammo;
        _segments = new Vector2[_segmentCount];
        
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _segmentCount;
        
        _animator.SetInteger(("AttackNumber"), attackNumber);
    }

    // Update is called once per frame
    void Update()
    {
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
         print((100 * Random.Range(0, 100))/100 <= 30);
    }

    void OnAbility(InputValue value)
    {
        if (Time.time > _lastShot + _fireRate && _ammo > 0)
        {
            if (value.isPressed)
            {
                arrowInHand = true;
                Instantiate(_arrowPrefab, _muzzle.transform.position, _muzzle.transform.rotation);
                _animator.SetBool("ArrowInHand", arrowInHand);
            }
            else if (arrowInHand && !value.isPressed)
            {
                arrowInHand = false;
                _animator.SetBool("ArrowInHand", arrowInHand);
            }
        
            
            _lastShot = Time.time;
            _ammo--;
        }
    }

    void OnLightAttack(InputValue value)
    {
        if (canAttack)
        {
            if (attackNumber == 1 && lastAttack <= 1)
            {
                attackNumber = 2;
                _animator.SetInteger(("AttackNumber"), attackNumber);
            }
            else
            {
                attackNumber = 1;
                _animator.SetInteger(("AttackNumber"), attackNumber);
            }
            _animator.SetTrigger("Punch");
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Vector3 castOrigin = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);
            if (Physics.Raycast(castOrigin, fwd, out RaycastHit hit, 1.5f))
            {
                
                if (hit.collider.gameObject.CompareTag("Ennemy"))
                {
                    if ((100 * Random.Range(0, 100))/100 <= bigAttackChancePourcentage && plugIn1)
                    {
                        Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                        rb.AddForce(gameObject.transform.forward * 150);
                        hit.collider.gameObject.GetComponent<AI_Stats>().looseHealth(_attackDamage+10);
                        _hitsnumber = 0;
                        Instantiate(_exploPrefab, hit.point, Quaternion.identity);
                        print("boom");
                    }
                    else
                    {
                        Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                        rb.AddForce(gameObject.transform.forward * 100);
                        hit.collider.gameObject.GetComponent<AI_Stats>().looseHealth(_attackDamage);
                        _hitsnumber++;
                    }
                
                }
            }
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
