using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject coinPrefab; // ✅ Added
    public GameObject magnetPrefab; // ✅ Added

    public Transform player;

    public float spawnDistance = 30f; // player ke aage kitni door spawn ho
    public float spawnRate = 2f;
    public float laneDistance = 2.5f;

    private float timer;

    void Update()
    {
        if (GameManager.instance.isGameOver) return; // added

        // Spawner ko player ke aage move karo
        transform.position = new Vector3(0, 0, player.position.z + spawnDistance);

        // Timer
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    void SpawnObstacle()
    {
        int lane = Random.Range(-1, 2); // -1,0,1 lanes

        Vector3 spawnPos = new Vector3(
            lane * laneDistance,
            1,
            transform.position.z
        );

        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        // ✅ Subway Surfers style coin line
        if (Random.value > 0.5f)
        {
            int coinLane = Random.Range(-1, 2);

            for (int i = 0; i < 5; i++)
            {
                Vector3 pos = new Vector3(
                    coinLane * laneDistance,
                    1.5f,
                    transform.position.z + i * 2
                );

                Instantiate(coinPrefab, pos, Quaternion.identity);
            }
        }

        // ✅ Magnet spawn chance (20%)
        if (Random.value > 0.8f)
        {
            int laneMagnet = Random.Range(-1, 2);

            Vector3 magnetPos = new Vector3(
                laneMagnet * laneDistance,
                1,
                transform.position.z
            );

            Instantiate(magnetPrefab, magnetPos, Quaternion.identity);
        }
    }
}