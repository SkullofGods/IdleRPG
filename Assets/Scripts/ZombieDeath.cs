using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class ZombieDeath : MonoBehaviour
{
    public Animator anim;
    public Health health;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        health.onDeath.AddListener(Die);
    }

    void Die()
    {
        Destroy(gameObject, .3f);
        Destroy(gameObject.GetComponent<Damage>());
        anim.SetBool("Dead", true);
    }
}
