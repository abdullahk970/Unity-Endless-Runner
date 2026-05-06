using UnityEngine;
using System.Collections;

public class MagnetPowerUp : MonoBehaviour
{
    public float duration = 5f;
    public float collectDistance = 2f;

    void Update()
    {
        if (PlayerMovement.instance == null) return;

        float distance = Vector3.Distance(
            transform.position,
            PlayerMovement.instance.transform.position
        );

        if (distance < collectDistance)
        {
            ActivateMagnet();
        }

        // rotation (optional)
        transform.Rotate(0, 100 * Time.deltaTime, 0);
    }

    void ActivateMagnet()
    {
        Debug.Log("MAGNET COLLECTED");

        // Activate magnet from Player script (better approach)
        PlayerMovement.instance.ActivateMagnet(duration);

        // OR if you strictly want coroutine here 👇
        // StartCoroutine(MagnetTimer());

        Destroy(gameObject);
    }

    IEnumerator MagnetTimer()
    {
        PlayerMovement.instance.magnetActive = true;

        yield return new WaitForSeconds(duration);

        if (PlayerMovement.instance != null)
            PlayerMovement.instance.magnetActive = false;
    }
}