using UnityEngine;

public class AI_Stats : MonoBehaviour
{
    [SerializeField] public float _maxHealth = 100f;

    public float _currentHealth;
    Animator _animator;
    PlayerStats _playerStats;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHealth <= 0)
        {
            death();
        }
    }

    public void looseHealth(float damage)
    {
        _animator.SetTrigger("HitReact");
        _currentHealth -= damage;
    }

    void death()
    {
        _playerStats.EnnemyKilled++;
        Destroy(gameObject);
        //_animator.SetTrigger("Death");
    }
}
