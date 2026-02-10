using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
public class AttackSystem : MonoBehaviour
{
    private int _ammo;
    private float _lastShot;

    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private GameObject _muzzle;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _attackDamage;
    void Start()
    {
        _ammo = gameObject.GetComponent<PlayerStats>()._ammo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAbility(InputValue value)
    {
        if (Time.time > _lastShot + _fireRate && _ammo > 0)
        {
            Instantiate(_arrowPrefab, _muzzle.transform.position, _muzzle.transform.rotation);
            _lastShot = Time.time;
            _ammo--;
        }
    }

    void OnLightAttack(InputValue value)
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 castOrigin = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);
        if (Physics.Raycast(castOrigin, fwd, out RaycastHit hit, 1.5f))
        {
            if (hit.collider.gameObject.CompareTag("Ennemy"))
            {
                Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(gameObject.transform.forward * 100);
                hit.collider.gameObject.GetComponent<AI_Stats>().looseHealth(_attackDamage);
            }
        }
        /*
        if (Physics.Raycast(transform.position, Vector3.forward, 2f ))
        {
            print("obj");
        }*/
            
    }
    private void OnDrawGizmos() 
    {
        Vector3 castOrigin = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(castOrigin,   fwd * 2);
    }
}
