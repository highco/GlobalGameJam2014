using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public PowerupType type;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spritesForTypes;
    public PowerUpSpawnPoint spawnPoint;

    public void SetupALook()
    {
        spriteRenderer.sprite = spritesForTypes[(int)type];
	}

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
