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
    private bool _canDash = true;
    private float _nextDashTime = 0;

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
        _movement.Move(mainHorizontal, mainVertical, dt);

        float secondaryHorizontal = Input.GetAxis("SecondaryHorizontal" + player.index);
        float secondaryVertical = Input.GetAxis("SecondaryVertical" + player.index);
        _shooter.Shoot(secondaryHorizontal, secondaryVertical, type, dt);

         
        const float dashTime = 0.1f;
        if (Input.GetAxis("TriggerAxis" + player.index) > 0.9)
        {
            if (_canDash && Time.time > _nextDashTime)
            {
                _movement.Dash(dashTime);
                _shooter.Dash(dashTime);
                _canDash = false;
                _nextDashTime = Time.time + dashTime * 6;
            }
        }
        else 
        {
            _canDash=true;
        }
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
				getHit(bullet);
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

	void getHit(Bullet bullet)
	{
		_health--;
		healthBar.ShowPercentage((float)_health / maxHealth);

		CameraShake main = (CameraShake)Camera.main.GetComponent("CameraShake");

		if (_health <= 0)
		{
			_gameController.CharacterHit(bullet, this);
			main.doShake(0.5f);
		}
		else
		{
            StartCoroutine(ShrinkBackAndForth(0.2f));
			main.doShake();
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
