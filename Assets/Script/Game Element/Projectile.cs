using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] float _speed;
    [SerializeField] float _damage;
    public GameObject parent;

    public bool Explosif;

    public GameObject _ExplosionPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchArrow()
    {
        
    }
    
     
        
        
        
    

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject + "+" + parent);
        if (collision.gameObject != parent)
        {
            if (collision.gameObject.CompareTag("Player") )
            {
                collision.gameObject.GetComponent<PlayerStats>().looseHealth(_damage);
            
            }
            else if (collision.gameObject.CompareTag("Ennemy"))
            {
                collision.gameObject.GetComponent<AI_Stats>().looseHealth(_damage);
            }

            if (Explosif)
            {
                Instantiate(_ExplosionPrefab, collision.contacts[0].point, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        
        
        
    }
}
