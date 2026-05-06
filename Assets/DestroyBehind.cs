using UnityEngine;

public class DestroyBehind : MonoBehaviour
{
    void Update()
    {
        if (transform.position.z < Camera.main.transform.position.z - 10f)
        {
            Destroy(gameObject);
        }
    }
}