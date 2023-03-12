    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    public Vector2 FightingAreaPoint;
    public float FightingAreaRadius;
    public float speed;

    public Vector2 pos;
    private void Start()
    {
        pos = FightingAreaPoint + Random.insideUnitCircle * FightingAreaRadius;
    }

    private void Update()
    {
        transform.Translate(((Vector3)pos - transform.position) * (speed * Time.deltaTime) );
    }

    
}
