
using UnityEngine;

public class Health : MonoBehaviour
{
    
    [SerializeField] int maxHealth = 25;
    public int _health;

    public Transform healthbar;


    IDamageable _damageable;

    void Awake()
    {
        _damageable = GetComponent<IDamageable>();
    }

    void Start()
    {
        _health = maxHealth;

        healthbar = Instantiate(EnemyManager.instance.healthbarPrefab, transform.position, Quaternion.identity).transform;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        UpdateHealthbar();
        

        _damageable.Damaged();
        if (_health <= 0)
        {
            _damageable.Die();
            Destroy(healthbar.gameObject);
        }
    }

    void Update()
    {
        float xPos = transform.position.x - (healthbar.localScale.x / 4);
        float yPos = transform.position.y - 0.5f;
        healthbar.position = new Vector3(xPos, yPos);
    }

    public void UpdateHealthbar()
    {
        float scale = (float)_health / maxHealth;
        healthbar.localScale = new Vector3(scale, healthbar.localScale.y, 1);
    }
}
