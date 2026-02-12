using UnityEngine;

public class AI_Stats : MonoBehaviour
{
    [SerializeField] public float _maxHealth = 100f;

    public float _currentHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void looseHealth(float damage)
    {
        _currentHealth -= damage;
    }
}
