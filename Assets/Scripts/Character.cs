using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public Player player;
    public CharacterType type;
    public int maxHealth = 4;
    public HealthBar healthBar;
    public SpriteRenderer spriteRenderer;

    private CharacterMovement _movement;
    private CharacterShooter _shooter;
    private GameController _gameController;
    private int _health;

    void Awake()
    {
        _movement = GetComponent<CharacterMovement>();
        _shooter = GetComponent<CharacterShooter>();
        _gameController = GameObject.FindObjectOfType<GameController>();
        _health = maxHealth;
        SetupLook();
    }

    public void DoUpdate(float dt)
    {
        float mainHorizontal = Input.GetAxis("Horizontal" + player.index);
        float mainVertical = Input.GetAxis("Vertical" + player.index);
        _movement.Move(mainHorizontal, mainVertical, dt);

        float secondaryHorizontal = Input.GetAxis("SecondaryHorizontal" + player.index);
        float secondaryVertical = Input.GetAxis("SecondaryVertical" + player.index);
        _shooter.Shoot(secondaryHorizontal, secondaryVertical, type, dt);
    }

    public void InitializeWithType(CharacterType aType)
    {
        type = aType;
        SetupLook();
    }

    void SetupLook()
    {
        spriteRenderer.material.color = player.color;
        _shooter.color = player.color;
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet.Type == type.GetPredator())
            {
                _health--;
                healthBar.ShowPercentage((float)_health / maxHealth);

                if (_health <= 0)
                    _gameController.CharacterHit(bullet);
            }
        }
    }

    public void Explode()
    {
        Destroy(this.gameObject);
    }
}
