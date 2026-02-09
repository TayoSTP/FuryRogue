using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float _currentHealth;
    public float _maxHealth = 100f;
    public GameObject respawnPoint;

    [SerializeField] private GameObject _playerPrefab;
    
    
    
    
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
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
        Destroy(gameObject);
        Instantiate(_playerPrefab, respawnPoint.transform.position, respawnPoint.transform.rotation);
    }

    public void gainHealth(float heal)
    {
        _currentHealth += heal;
    }
}
