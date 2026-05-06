using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab;
    public Transform player;

    public float groundLength = 30f;
    private float spawnZ = 0f;

    void Start()
    {
        // Start mein 2–3 tiles spawn karo
        for (int i = 0; i < 3; i++)
        {
            SpawnGround();
        }
    }

    void Update()
    {
        if (player.position.z > spawnZ - (groundLength * 2))
        {
            SpawnGround();
        }
    }

    void SpawnGround()
    {
        Instantiate(groundPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
        spawnZ += groundLength;
    }
}