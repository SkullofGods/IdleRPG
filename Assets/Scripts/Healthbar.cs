using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Health _Health;
    public Image healthbar;
    
    private float maxHealth;

    private void Start()
    {
        _Health = GetComponentInParent<Health>();
        maxHealth = _Health.HealthPoints;
    }

    private void Update()
    {
        healthbar.fillAmount = _Health.HealthPoints / maxHealth;
    }
}
