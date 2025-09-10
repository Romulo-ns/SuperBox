using UnityEngine;

public class PlatformLife : MonoBehaviour
{
    public Transform player;       // Reference to player
    public float destroyDistance = 20f; // How far behind before destroying

    void Update()
    {
        if (player != null && transform.position.x < player.position.x - destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
