using UnityEngine;

public class DeathZoneFollow : MonoBehaviour
{
    public Transform player;
    public float fixedY = -10f; // fixed death line height

    void Update()
    {
        if (player != null)
        {
            // Follow only on X, keep Y fixed
            transform.position = new Vector3(player.position.x, fixedY, 0f);
        }
    }
}
