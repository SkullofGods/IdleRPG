using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damage : MonoBehaviour
{
    public float damage;
    public float AttackRadius;
    public float AttackSpeed = 0.5f;
    public string AttackTag;
    public Animator anim;
    public UnityEvent onAttack;
    
    private float timer = 0;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= AttackSpeed)
        {
            var colls = Physics2D.OverlapCircleAll(transform.position, AttackRadius);
            foreach (var col in colls)
            {
                Component health;
                if (col.GetComponent<Health>() != null && col.gameObject.tag.Equals(AttackTag))
                {
                    onAttack?.Invoke();
                    col.GetComponent<Health>().TakeDamage(damage);
                }
            }

            timer = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }
}
