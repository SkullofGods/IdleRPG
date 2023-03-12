using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;
    public float SpawnRadius;
    public float timeToSpawn;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToSpawn)
        {
            // var enemy = Instantiate(Enemy, transform.position, Quaternion.identity);
            SpawnSphereOnEdgeRandomly2D();
            timer = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, SpawnRadius);
    }

    private void SpawnSphereOnEdgeRandomly2D()
    {
        var radius = SpawnRadius;
        var randomPos = Random.insideUnitSphere * radius;
        randomPos += transform.position;
        randomPos.y = 0f;

        var direction = randomPos - transform.position;
        direction.Normalize();

        var dotProduct = Vector3.Dot(transform.forward, direction);
        var dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * radius + transform.position.x;
        randomPos.y = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * radius + transform.position.y;
        randomPos.z = transform.position.z;

        var go = Instantiate(Enemy, randomPos, Quaternion.identity);
        go.transform.position = randomPos;
    }
}