using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    private bool isUsed;
    private float cooldown = 5f;
    private float startTime;

    private float timer = 5f;
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(ControlIt);
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
    
    void ControlIt()
    {
        if (timer >= cooldown)
        {
            timer = 0f;
            isUsed = true;
            GetComponent<Button>().interactable = false;
            var controlledEnemy = FindObjectsOfType<Enemy>()[Random.Range(0, FindObjectsOfType<Enemy>().Length)]
                .gameObject;
            controlledEnemy.tag = "Controlled";
            controlledEnemy.GetComponent<Enemy>().onControl = true;
            controlledEnemy.GetComponent<Damage>().AttackTag = "Enemy";
            controlledEnemy.GetComponentInChildren<SpriteRenderer>().color = Color.magenta;
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
