using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    public float _currentHealth;
    public float _maxHealth = 100f;
    public GameObject respawnPoint;
    public int _ammo = 3;
    public int scraps;
    public int water;

    private bool canDrink = true;
    [SerializeField] private GameObject _playerPrefab;
    
    
    
    
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }

    public void looseHealth(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            death();
        }
    }

    void death()
    {
    	_currentHealth = _maxHealth;
        gameObject.transform.position = respawnPoint.transform.position;
    }

    void OnDrink()
    {
        if (canDrink)
        {
            DecreaseWater();
            canDrink = false;
            Invoke("DecreaseWater", 2f);
        }
        
    }

    void ResetDrink()
    {
        canDrink = true;
    }

    public void gainHealth(float heal)
    {
        _currentHealth += heal;
    }

    public void DecreaseWater()
    {
        if (water > 0)
        {
            water -= 50;
        }
    }

    public void IncreaseWater(int amount)
    {
        water += amount;
    }
    public void IncreaseScraps(int amount)
    {
        scraps += amount;
    }
}
