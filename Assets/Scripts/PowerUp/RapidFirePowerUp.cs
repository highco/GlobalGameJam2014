using UnityEngine;
using System.Collections;

public class RapidFirePowerUp : PowerUp {

	public float lifetimeInSeconds = 5f;
	public float lifeCounter = 0f;

	private const float FAST_SHOOT_SPEED = 0.2f;
	private float defaultTime;


	private bool _alive = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CharacterShooter cs = transform.parent.gameObject.GetComponent<CharacterShooter>();

		if(!_alive)
		{
			defaultTime = cs.reloadTime;
			cs.reloadTime = 0.2f;
			_alive = true;
		}
		else
		{
			lifeCounter += Time.deltaTime;

			if(lifeCounter > lifetimeInSeconds)
			{
				cs.reloadTime = defaultTime;
				Destroy(this);
			}
		}
	}
}
