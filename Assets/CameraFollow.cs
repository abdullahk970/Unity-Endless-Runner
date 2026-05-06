using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;   // player reference
    public Vector3 offset;     // distance from player

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            player.position + offset,
            5f * Time.deltaTime
        );
    }
}