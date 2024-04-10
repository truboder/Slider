using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _speedChange; 
    [SerializeField] private float _damage;

    private float _currentHealth;
    private float _inaccuracy = 0.1f;

    void Start()
    {
        _currentHealth = _maxHealth;
        _slider.maxValue = _maxHealth;
        _slider.value = _currentHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StopAllCoroutines();
            _currentHealth -= _damage;
            StartCoroutine(ChangeHealth(x => x.value >= _currentHealth + _inaccuracy));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StopAllCoroutines();
            _currentHealth += _damage;
            StartCoroutine(ChangeHealth(x => x.value <= _currentHealth - _inaccuracy));
        }
    }

    private IEnumerator ChangeHealth(Predicate<Slider> predicate)
    {
        while (predicate.Invoke(_slider))
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _currentHealth, Time.deltaTime * _speedChange);

            yield return null;
        }
        _slider.value = _currentHealth;
    }
}