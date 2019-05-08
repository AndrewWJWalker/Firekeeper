using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int _healthPoints;

    [SerializeField] public int maxHealth;
    //public int maxHealth { get; private set; }

    //private void Start()
    //{
    //    maxHealth = _healthPoints;
    //}

    public void RestoreHealth()
    {
        _healthPoints = maxHealth;
    }

    public void DealDamage(int damage)
    {
        _healthPoints -= damage;

        if (_healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int GetCurrentHealthPoints()
    {
        return _healthPoints;
    }
}
