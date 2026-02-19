using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public float _currentHealth;
    public float _maxHealth = 100f;
    public GameObject respawnPoint;
    public int _ammo = 3;
    public int scraps;
    public float maxWater;
    public float water;
    public float maxStamina;
    public float currentStamina;
    public int EnnemyKilled;

    private bool canDrink = true;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject defeatUI;

    public WaterSource _waterSource;
    public Upgrade _upgradeScript;
    public GameObject _upgradeUI;
    void Start()
    {
        _currentHealth = _maxHealth;
        currentStamina = maxStamina;
        water = maxWater;
    }

    private void Update()
    {
        _currentHealth= Mathf.Clamp(_currentHealth, 0, _maxHealth);
        water = Mathf.Clamp(water, 0, maxWater);
        _ammo = Mathf.Clamp(_ammo, 0, _ammo);
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        print(_currentHealth);
    }

    public void looseHealth(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            StartCoroutine(death());
        }
    }

    IEnumerator death()
    {
        defeatUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        gameObject.transform.position = respawnPoint.transform.position;
        _currentHealth = _maxHealth;
        defeatUI.SetActive(false);
        gameObject.SetActive(true);
        
        
        
    }

    void OnDrink()
    {
        if (canDrink)
        {
            canDrink = false;
            DecreaseWater();
            gainHealth((_currentHealth*50)/100);
            Invoke("ResetDrink", 2f);
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
            print("drinking");
            water -= 50;
        }
    }

    void OnInteract()
    {
        if (_waterSource != null)
        {
            RefillWater();
        }

        if (_upgradeScript != null)
        {
            _upgradeUI.SetActive(true);
            _upgradeScript.OpenMenu();
            _upgradeScript.UI.SetActive(false);
        }
    }
    public void RefillWater()
    {
        water = maxWater;
    }
    public void IncreaseScraps(int amount)
    {
        scraps += amount;
    }
}
