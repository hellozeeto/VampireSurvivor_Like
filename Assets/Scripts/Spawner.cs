using UnityEngine;

public class Spawner : MonoBehaviour
{
    public PoolManager pool;
    public Transform player; // 플레이어의 위치 참조

    public float minRad = 8f;
    public float maxRad = 11f;
    
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        Vector3 spawnPosition = GetRandomPointAroundPlayer(player.position, minRad, maxRad);
        pool.Get(0).transform.position = spawnPosition;
    }

    Vector3 GetRandomPointAroundPlayer(Vector3 playerPosition, float minRadius, float maxRadius)
    {
        float randomRadius = Random.Range(minRadius, maxRadius);
        float randomAngle = Random.Range(0, 2 * Mathf.PI);

        float xOffset = randomRadius * Mathf.Cos(randomAngle);
        float zOffset = randomRadius * Mathf.Sin(randomAngle);

        Vector3 spawnPosition = new Vector3(playerPosition.x + xOffset, playerPosition.y, playerPosition.z + zOffset);
        return spawnPosition;
    }
}