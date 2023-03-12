using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject Boss;
    private void Start()
    {
        
            SpawnSphereOnEdgeRandomly2D();
    }
    
    private void SpawnSphereOnEdgeRandomly2D()
    {
        float radius = 4;
        Vector3 randomPos = Random.insideUnitSphere * radius;
        randomPos += transform.position;
        randomPos.y = 0f;
    
        Vector3 direction = randomPos - transform.position;
        direction.Normalize();
    
        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);
    
        randomPos.x = Mathf.Cos(dotProductAngle) * radius + transform.position.x;
        randomPos.y = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * radius + transform.position.y;
        randomPos.z = transform.position.z;
    
        GameObject go = Instantiate(Boss, randomPos, Quaternion.identity);
        go.transform.position = randomPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 4);
    }
}
