using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int playerIndex;
    public PlayerType type;

    private CharacterMovement _movement;
    private CharacterShooter _shooter;

    void Awake()
    {
        _movement = GetComponent<CharacterMovement>();
        _shooter = GetComponent<CharacterShooter>();
        SetupLook();
    }

    void Update()
    {
        float mainHorizontal = Input.GetAxis("Horizontal" + playerIndex);
        float mainVertical = Input.GetAxis("Vertical" + playerIndex);
        _movement.Move(mainHorizontal, mainVertical);

        float secondaryHorizontal = Input.GetAxis("SecondaryHorizontal" + playerIndex);
        float secondaryVertical = Input.GetAxis("SecondaryVertical" + playerIndex);
        _shooter.Shoot(secondaryHorizontal, secondaryVertical, type);
    }

    public void InitializeWithType(PlayerType aType)
    {
        type = aType;
        SetupLook();
    }

    void SetupLook()
    {
        renderer.material.color = type.ToColor();
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            print("Hit!");
        }
    }
}
