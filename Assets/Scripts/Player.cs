using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHealth
{
    private const float Inaccuracy = 0.1f;

    [SerializeField] private float _speedChange;
    [SerializeField] private float _currentHealth;
    
    private float _health;

    public float Health 
    {
        get { return _health; }

        private set 
        {
            _health = value;
            OnHealthChange?.Invoke(_health);
        }
    }

    public event Action<float> OnHealthChange;

    private void Awake()
    {
        Health = _currentHealth;
    }

    private IEnumerator ChangeHealth(Predicate<float> predicate)
    {
        while (predicate.Invoke(Health))
        {
            Health = Mathf.MoveTowards(Health, _currentHealth, Time.deltaTime * _speedChange);

            yield return null;
        }
        Health = _currentHealth;
    }

    public void Hit(float damage)
    {
        StopAllCoroutines();
        _currentHealth -= damage;
        StartCoroutine(ChangeHealth(x => x >= _currentHealth + Inaccuracy));
    }

    public void Heal(float heal)
    {
        StopAllCoroutines();
        _currentHealth += heal;
        StartCoroutine(ChangeHealth(x => x <= _currentHealth - Inaccuracy));
    }
}
