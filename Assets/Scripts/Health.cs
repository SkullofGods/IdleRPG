using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float HealthPoints = 100f;
    public float MaxHealthPoints;
    public UnityEvent onDeath;
    public Animator anim;

    private void Start()
    {
        MaxHealthPoints = HealthPoints;
    }

    public void TakeDamage(float damage)
    {
        if (gameObject.CompareTag("Enemy"))
        {
            anim.SetTrigger("Hit");
            damage += (MaxHealthPoints * 0.05f);
            GameObject.FindWithTag("Player").GetComponent<Health>().HealthPoints += (MaxHealthPoints * 0.05f);
        }
        if (HealthPoints - damage <= 0)
        {
            Die();
            HealthPoints = 0;
            return;
        }

        HealthPoints -= damage;
    }

    public void Die()
    {
        onDeath?.Invoke();
        // Destroy(gameObject);
    }
}
