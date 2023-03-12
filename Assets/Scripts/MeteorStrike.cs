using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MeteorStrike : MonoBehaviour
{
    public float radius;
    public GameObject meteor;
    public Transform player;
    public float damage = 100;
    public float cooldown = 10f;
    private float startTime;
    private bool isUsed;
    private float timer = 10f;
    
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(Strike);
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

    public void Strike()
    {
        if (timer >= cooldown)
        {
            timer = 0f;
            isUsed = true;
            GetComponent<Button>().interactable = false;
            var pos = Random.insideUnitCircle * radius;
            var m = Instantiate(meteor, new Vector3(pos.x, 100), Quaternion.identity);
            LeanTween.moveY(m, pos.y, .5f).setOnComplete(
                () =>
                {
                    Destroy(m, .2f);
                    DealDamage();
                });
        }

    }

    public void DealDamage()
    {
        var allColliders = Physics2D.OverlapCircleAll(player.position, radius);
        List<Collider2D> nonPlayerColliders = new List<Collider2D>();
        foreach (var collider in allColliders)
        {
            if (collider.tag.Equals("Enemy"))
                nonPlayerColliders.Add(collider);
        }
        if (nonPlayerColliders.Count > 0)
        {
            foreach (var enemy in nonPlayerColliders)
            {
                enemy.GetComponent<Health>().TakeDamage(damage + nonPlayerColliders.Count * 10);
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
            this.GetComponent<Image>().fillAmount = Mathf.Lerp(0f, 1f, fraction); 
            yield return null; 
        }
    }
}

