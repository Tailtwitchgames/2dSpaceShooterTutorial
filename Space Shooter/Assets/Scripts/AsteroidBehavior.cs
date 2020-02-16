using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{


	//Put the asteroid somewhere on the screen
	//shoot it first
	//then enemies start spawning
	private float _rotateSpeed = 3.0f;
	[SerializeField] private GameObject _explosionPrefab;
	private SpawnManager _spawnManager;
	private AudioManager _audioManager;
	// Start is called before the first frame update
	void Start()
	{
		_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Laser")
			{
			//Debug.Log("Asteroid Destroyed Logic Activated");
			Destroy(other.gameObject);
			//Debug.Log("Destroy Laser.");
			Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
			_audioManager.ExplosionSound();
			//Debug.Log("Create Explosion.");
			_spawnManager.AsteroidDestroyed();
			//Debug.Log("AsteroidDestroyed Method Called.");
			Destroy(this.gameObject);
			//Debug.Log("Destroy Asteroid");

			
			}
	}
	// Update is called once per frame
	void Update()
    {
		//rotate along z axis, 3m / sec, give or take, transform.rotate
		transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }
}
