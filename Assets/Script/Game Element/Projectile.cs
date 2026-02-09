using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] float _speed;
    [SerializeField] float _damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * _speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStats>().looseHealth(_damage);
            
        }
        Destroy(gameObject);
    }
}
