using UnityEngine;
using System.Collections;

public class RapidFirePowerUp : PowerUp {

	public float lifetimeInSeconds = 5f;
	public float lifeCounter = 0f;

	private const float FAST_SHOOT_SPEED = 0.02f;
	private float defaultTime;


	private bool _alive = false;
	
	// Update is called once per frame
	void Update () {
		CharacterShooter cs = transform.gameObject.GetComponent<CharacterShooter>();

		Debug.Log("wat");
		if(!_alive)
		{
			defaultTime = cs.reloadTime;
			cs.reloadTime = FAST_SHOOT_SPEED;
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
