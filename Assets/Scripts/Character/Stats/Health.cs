using System;
using UnityEngine;

public class Health: MonoBehaviour, IDamagable
{
    public event Action ReceivedDamage;
    public event Action Died;

    [SerializeField, Range(1,10)] private int _maxHealth;
    private int _currentHealth;

    public void Initialize() => _currentHealth = _maxHealth;

    public void TakeDamage()
    {
        _currentHealth--;

        if (_currentHealth <= 0)
        {
            Died?.Invoke();
            return;
        }

        ReceivedDamage?.Invoke();
    }
}
