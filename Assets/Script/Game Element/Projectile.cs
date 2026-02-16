using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] float _speed;
    [SerializeField] float _damage;

    public bool Explosif;

    public GameObject _ExplosionPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchArrow()
    {
        StartCoroutine(Launch());
    }
    
     IEnumerator Launch()
    {
        yield return new WaitForSeconds(0.2f);
        _rb.useGravity = true;
        _rb.AddForce(transform.forward * _speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStats>().looseHealth(_damage);
            
        }

        if (Explosif)
        {
            Instantiate(_ExplosionPrefab, collision.contacts[0].point, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}
