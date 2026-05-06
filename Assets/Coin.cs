using UnityEngine;

public class Coin : MonoBehaviour
{
    void Update()
    {
        if (PlayerMovement.instance == null) return;

        Transform player = PlayerMovement.instance.transform;

        // NORMAL COLLECT
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < 2f)
        {
            Collect();
        }

        // MAGNET EFFECT
        if (PlayerMovement.instance.magnetActive)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                20f * Time.deltaTime
            );
        }

        // rotation
        transform.Rotate(0, 100 * Time.deltaTime, 0);
    }

    void Collect()
    {
        GameManager.instance.AddCoin();
        Destroy(gameObject);
    }
}