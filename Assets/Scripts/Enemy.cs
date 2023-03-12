    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public Vector2 FightingAreaPoint;
    public float FightingAreaRadius;
    public float speed;
    public bool onControl;

    public Vector2 pos;
    private void Start()
    {
        pos = FightingAreaPoint + Random.insideUnitCircle * FightingAreaRadius;
    }

    private void Update()
    {
        if(!onControl)
            transform.Translate(((Vector3)pos - transform.position) * (speed * Time.deltaTime) );
        else
            transform.Translate((GameObject.FindWithTag("Enemy").transform.position - transform.position) * (speed * Time.deltaTime) );
        if (onControl)
            StartCoroutine(Controlled());
    }

    IEnumerator Controlled()
    {
        yield return new WaitForSeconds(5);
        onControl = false;
        tag = "Enemy";
        GetComponent<Damage>().AttackTag = "Player";
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
