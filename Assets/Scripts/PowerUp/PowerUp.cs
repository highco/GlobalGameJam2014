using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public PowerupType type;
    public SpriteRenderer spriteRenderer;
    public PowerUpSpawnPoint spawnPoint;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spawnPoint.occupier = null;
            collision.gameObject.GetComponent<Character>().PickedUpPowerup(type);
            Destroy(gameObject);
        }
    }
}
