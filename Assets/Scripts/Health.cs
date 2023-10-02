using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _minHealth;
    private int _health;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void Damage(int _damage)
    {
        _health -= _damage;
        if (_health < _minHealth)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        Debug.Log(gameObject.name + " has died.");
        Destroy(gameObject);
    }
}
