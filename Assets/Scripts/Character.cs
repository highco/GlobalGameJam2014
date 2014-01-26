﻿using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public Player player;
    public CharacterType type;
    public int maxHealth = 4;
    public HealthBar healthBar;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spritesForTypes;
    public GameObject deathFXPrefab;

    private CharacterMovement _movement;
    private CharacterShooter _shooter;
    private GameController _gameController;
    private int _health;
    private bool _stopped;
    private Vector3 _normalScale;

    void Awake()
    {
        _movement = GetComponent<CharacterMovement>();
        _shooter = GetComponent<CharacterShooter>();
        _gameController = GameObject.FindObjectOfType<GameController>();
        _health = maxHealth;
        _normalScale = transform.localScale;
    }

    public void DoUpdate(float dt)
    {
        if (_stopped)
            return;

        float mainHorizontal = Input.GetAxis("Horizontal" + player.index);
        float mainVertical = Input.GetAxis("Vertical" + player.index);
        if (Mathf.Abs(mainHorizontal) > 0.1 || Mathf.Abs(mainVertical) > 0.1)
            _movement.Move(mainHorizontal, mainVertical, dt);
        else
            _movement.Move(0, 0, dt);

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
        spriteRenderer.sprite = spritesForTypes[(int)type];
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
                    _gameController.CharacterHit(bullet, this);
                else
                    StartCoroutine(ShrinkBackAndForth(0.2f));
            }
            else if (bullet.Type == type.GetVictim())
            {
                _health++;
                if (_health > maxHealth)
                    _health = maxHealth;
                healthBar.ShowPercentage((float)_health / maxHealth);
            }
        }
    }

    IEnumerator ShrinkBackAndForth(float time)
    {
        transform.localScale = _normalScale;
        iTween.ScaleTo(transform.gameObject, 0.5f * _normalScale, time / 3f);
        yield return new WaitForSeconds(time / 3f);
        iTween.ScaleTo(transform.gameObject, _normalScale, 2f * time / 3f);
    }

    public void Explode()
    {
        Destroy(this.gameObject);
        GameObject fx = Instantiate(deathFXPrefab, transform.position, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
        fx.GetComponent<SpriteRenderer>().color = player.color;
        Destroy(fx, 9f / 24f);
    }

    public void Stop()
    {
        rigidbody.velocity = Vector3.zero;
        _stopped = true;
    }

    public void PickedUpPowerup(PowerupType powerUpType)
    {
        if (powerUpType == PowerupType.Health)
        {
            _health = maxHealth;
            healthBar.ShowPercentage((float)_health / maxHealth);
        }
        else 
        {
            type = powerUpType.ToCharacterType();
            SetupLook();
        }
    }
}
