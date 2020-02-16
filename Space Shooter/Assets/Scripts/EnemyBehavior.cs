using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
	private float _enemySpeed = 4.0f;
	private Player _player;
	//handle to animator component
	private Animator _onDeathAnimation;
	private AudioManager _audioManager;
	[SerializeField] private GameObject _enemyLaserPrefab;
	private bool _isDead = false;
	private GameObject _camera;

	// Start is called before the first frame update
	void Start()
	{
		_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		_player = GameObject.Find("Player").GetComponent<Player>();
		_camera = GameObject.Find("Main Camera");
		if (_player == null)
		{
			Debug.LogError("The Player is NULL.");
		}
		_onDeathAnimation = GetComponent<Animator>();
		if (_onDeathAnimation == null)
		{
			Debug.LogError("The Animator is NULL.");
		}
		if (_camera == null)
		{
			Debug.LogError("The Camera is NULL.");
		}
		StartCoroutine(EnemyFire());
		//nullcheck for player
		//assign handle for anim
	}

	// Update is called once per frame
	void Update()
	{
		float onScreenX = Random.Range(-10f, 10f);

		//move down at 4m / sec
		//if below bottom of screen
		//respawn at top with new random x
		transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

		if (transform.position.y < -5)
		{
			transform.position = new Vector3(onScreenX, 7, 0);
		}
	}
	private IEnumerator EnemyFire()
	{
		if (_isDead == false)
		{
			yield return new WaitForSeconds(Random.Range(0.5f, 2f));
			Instantiate(_enemyLaserPrefab, new Vector3(transform.position.x + 0.25f, transform.position.y - 0.25f, 0), Quaternion.identity);
			_audioManager.LaserFireSound();
		}
		else
		{
			yield return new WaitForSeconds(5f);
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Player player = other.transform.GetComponent<Player>();
			if (player != null)
			{
				player.Health();
				_audioManager.ExplosionSound();
			}
			//animation trigger
			_onDeathAnimation.SetTrigger("OnEnemyDeath");
			_isDead = true;
			StopCoroutine(EnemyFire());
			_enemySpeed = 0;
			Destroy(GetComponent<Collider2D>());
			Destroy(this.gameObject, 2.4f);
			_camera.GetComponent<CameraShake>().TriggerShake();
				
		}

		if (other.tag == "Laser")
		{
			//Debug.Log("Collision Detected");
			Destroy(other.gameObject);
			_isDead = true;
			Destroy(GetComponent<Collider2D>());
			_audioManager.ExplosionSound();
			if (_player != null)
			{
				_player.AddToScore(10);
			}
			//animation trigger
			_enemySpeed = 0f;
			_onDeathAnimation.SetTrigger("OnEnemyDeath");
			Destroy(this.gameObject, 2.4f);
		}
	}
}
