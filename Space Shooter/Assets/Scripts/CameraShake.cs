using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	private Transform _transform;
	private float _shakeDuration = 0f;
	private float _shakeMagnitude = 0.5f;
	private float _dampeningSpeed = 1f;
	Vector3 _initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }
	private void Awake()
	{
		if (_transform == null)
		{
			_transform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	private void OnEnable()
	{
		_initialPosition = transform.localPosition;
	}
	void Update()
    {
		if (_shakeDuration > 0)
		{
			transform.localPosition = _initialPosition + Random.insideUnitSphere * _shakeMagnitude;
			_shakeDuration -= Time.deltaTime * _dampeningSpeed;
		}
		else
		{
			_shakeDuration = 0f;
			transform.localPosition = _initialPosition;
		}
    }
	public void TriggerShake()
	{
		_shakeDuration = .5f;
	}
}
