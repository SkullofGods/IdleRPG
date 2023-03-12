using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LightningStrike : MonoBehaviour
{
    public float Damage;
    public float Radius;
    public Transform player;
    public int bounceTimes;
    public float cooldown;
    public List<GameObject> strikedEnemies;
    public GameObject Spark;
    private float startTime;
    private bool isUsed;
    private float timer = 5f;
    
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(LightningStrikee);
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        GetComponent<Image>().fillAmount = timer / cooldown;
        if (timer >= cooldown)
        {
            GetComponent<Button>().interactable = true;
            isUsed = false;
        }
    }
            
    public void LightningStrikee()
    {
        if (timer >= cooldown)
        {
            timer = 0f;
            isUsed = true;
            GetComponent<Button>().interactable = false;
            Strike(bounceTimes);
            strikedEnemies.Clear();
        }
       
    }

    void Strike(int numberOfBounces)
    {
        Debug.Log("STRIKE");
        var allColliders = Physics2D.OverlapCircleAll(player.position, Radius);
        List<Collider2D> nonPlayerColliders = new List<Collider2D>();
        foreach (var collider in allColliders)
        {
            if (collider.tag.Equals("Enemy") && !strikedEnemies.Contains(collider.gameObject))
                nonPlayerColliders.Add(collider);
        }
        if (nonPlayerColliders.Count > 0)
        {
            var randEnemy = nonPlayerColliders[(int)Random.Range(0, nonPlayerColliders.Count -1)];
            strikedEnemies.Add(randEnemy.gameObject);
            Instantiate(Spark, randEnemy.transform);
            randEnemy.GetComponent<Health>().TakeDamage(Damage);
            if (numberOfBounces > 0)
            {
                Strike(numberOfBounces-1);
            }
        }
    }
    
    IEnumerator Cooldown()
    {
        startTime = Time.realtimeSinceStartup; 
        float fraction = 0f;
        while(fraction < 1f) 
        { 
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / cooldown); 
            GetComponent<Image>().fillAmount = Mathf.Lerp(0f, 1f, fraction); 
            yield return null; 
        }
    }
}