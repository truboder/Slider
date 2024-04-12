using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    
    [SerializeField] private Player _unit;

    private void Start()
    {
        _slider.maxValue = _unit.Health;
        _slider.value = _unit.Health;

        _unit.OnHealthChange += UpdateHealth;
    }

    private void OnDestroy()
    {
        _unit.OnHealthChange -= UpdateHealth;
    }

    private void UpdateHealth(float health)
    {
        _slider.value = health;
    }
}