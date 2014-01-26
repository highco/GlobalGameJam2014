using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	private const float DEFAULT_DECAY = 0.01f;
	private const float DEFAULT_INTENSITY = 0.1f;

	public float magnitude;
	public float shakeDecay;

	private Vector3 _originalPosition;
	private bool _shaking = false;
	// Use this for initialization
	void Start () 
	{
		shakeDecay = DEFAULT_DECAY;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(magnitude > 0)
		{
			transform.position = _originalPosition + Random.insideUnitSphere * magnitude;

			magnitude -= shakeDecay;
		}
		else
		{
			if(_shaking)
			{
			    _shaking = false;
				transform.position = _originalPosition;
			}

		}
	
	}

	public void doShake()
	{
		doShake(DEFAULT_INTENSITY);
	}

	public void doShake(float intensity)
	{
		doShake(intensity, DEFAULT_DECAY);
	}

	public void doShake(float intensity, float time)
	{
		magnitude = intensity;

		if(!_shaking)
		{
			_originalPosition = transform.position;
		}
	}
}
