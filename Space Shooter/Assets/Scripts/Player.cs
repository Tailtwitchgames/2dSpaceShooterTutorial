using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private float _playerBaseSpeed = 5f;
	public GameObject laserObject;
	private float _fireRate = 0.15f;
	private float _nextFire = 0.0f;
	[SerializeField] private int _health = 3;
	private SpawnManager _spawnManager;
	public GameObject Triple_Shot;
	private bool _tripleShotActive = false;
	private int _tripleShotCount = 50;
	[SerializeField] private bool _isShieldActive;
	//reference shield object
	[SerializeField] private GameObject _shieldVisualizer;
	[SerializeField] private int _score;
	private UIManager _uiManager;
	[SerializeField] private GameObject _healthFlameLeft;
	[SerializeField] private GameObject _healthFlameRight;
	private AudioManager _audioManager;
	private float _playerBoostSpeed = 1.5f;
	private float _playerSpeed;
	private int _shieldHitCounter;
	private int ammoCount = 15;
	private bool _haveGrenade;


	// Start is called before the first frame update

	void Start()
	{
		//assign current position = 0,-4,0
		transform.position = new Vector3(0, -4, 0);
		_healthFlameLeft.SetActive(false);
		_healthFlameRight.SetActive(false);
		_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		if (_spawnManager == null)
		{
			Debug.LogError("The Spawn Manager is not functional.");
		}
		if (_uiManager == null)
		{
			Debug.LogError("The UI Manager is not functional.");
		}
	}

	// Update is called once per frame
	void Update()
	{
		bool boosterOn = Input.GetKey(KeyCode.LeftShift);
		if (boosterOn == true)
		{
			_playerSpeed = _playerBaseSpeed * _playerBoostSpeed;
		}
		else
		{
			_playerSpeed = _playerBaseSpeed;
		}
		PlayerMovement();
		ShieldOpacity();
		GrenadeFire();
		if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire)
		{
			LaserFire();
		}
		if (Input.GetKey(KeyCode.Escape) == true)
		{
			Application.Quit();
		}
	}

	void PlayerMovement()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		transform.Translate(Vector3.right * _playerSpeed * horizontalInput * Time.deltaTime);
		transform.Translate(Vector3.up * _playerSpeed * verticalInput * Time.deltaTime);
		float escapeToQuit = Input.GetAxis("Cancel");
		float _edgeOfScreen = 9.5f;



		//if player position on y > 0
		//y position = 0
		if (transform.position.y >= 0)
		{
			transform.position = new Vector3(transform.position.x, 0, 0);
		}
		else if (transform.position.y <= -3.8f)
		{
			transform.position = new Vector3(transform.position.x, -3.8f, 0);
		}

		//if player on the x > 11, send to -11
		//else if playr on the x < -11 send to 11
		if (transform.position.x >= _edgeOfScreen)
		{
			transform.position = new Vector3(_edgeOfScreen, transform.position.y, 0);
		}
		else if (transform.position.x <= (_edgeOfScreen * -1))
		{
			transform.position = new Vector3((_edgeOfScreen * -1), transform.position.y, 0);
		}
	}
	void LaserFire()
	{
		{
			_nextFire = Time.time + _fireRate;
			if (_tripleShotActive == true)
			{
				Instantiate(Triple_Shot, new Vector3((transform.position.x - 0.625f), (transform.position.y + 0.155f), 0), Quaternion.identity);
				_audioManager.LaserFireSound();
				_tripleShotCount--;
				if (_tripleShotCount < 1)
				{
					_tripleShotActive = false;
				}
			}
			else if (ammoCount > 0)
			{
				Instantiate(laserObject, new Vector3(transform.position.x, (transform.position.y + 1.05f), 0), Quaternion.identity);
				_audioManager.LaserFireSound();
				ammoCount--;
			}
			else
			{
				_audioManager.EmptyAmmoSound();
			}
		}
	}
	public void Health()
	{
		//if shields active
		//deactivate shields
		//return;
		if (_isShieldActive == true)
		{
			_shieldHitCounter--;
			return;
		}
		_health--;
		_audioManager.ExplosionSound();
		_uiManager.UpdateLives(_health);
		if (_health < 1)
		{
			_spawnManager.OnPlayerDeath();
			_uiManager.GameOverDisplay();
			Destroy(this.gameObject);
		}
		if (_health == 2)
		{
			switch (Random.Range(0, 2))
			{
				case 0:
					_healthFlameLeft.SetActive(true);
					break;
				case 1:
					_healthFlameRight.SetActive(true);
					break;
			}
			if (_health == 1)
			{
				_healthFlameLeft.SetActive(true);
				_healthFlameRight.SetActive(true);
			}
		}
	}
	public void TripleShotActivate()
	{

		_tripleShotActive = true;
		StartCoroutine(TripleShotTimer());
	}
	IEnumerator TripleShotTimer()
	{

		yield return new WaitForSeconds(5f);
		_tripleShotActive = false;
		StopCoroutine(TripleShotTimer());
	}
	public void SpeedUp()
	{
		StartCoroutine(SpeedUpTimer());
	}
	IEnumerator SpeedUpTimer()
	{
		_playerSpeed = 8.5f;
		//Debug.Log("Your speed is increased.");
		yield return new WaitForSeconds(5f);
		_playerSpeed = 5f;
		//Debug.Log("Your speed is normal.");
		StopCoroutine(SpeedUpTimer());
	}
	public void ShieldsActive()
	{
		_isShieldActive = true;
		_shieldHitCounter = 3;
		_shieldVisualizer.SetActive(true);
	}
	public void AddToScore(int points)
	{
		//method to add 10 to score
		_score += points;
		_uiManager.UpdateScore(_score);
		//communicate with UI mnger and update

	}
	public void ShieldOpacity()
	{
		var _shieldOpacity = _shieldVisualizer.GetComponent<SpriteRenderer>();

		if (_shieldHitCounter == 3)
		{
			//shield opacity = 100%
			_shieldOpacity.color = new Color(1f, 1f, 1f, 1f);
		}
		else if (_shieldHitCounter == 2)
		{
			//shield opacity = 66%
			_shieldOpacity.color = new Color(1f, 1f, 1f, .6f);
		}
		else if (_shieldHitCounter == 1)
		{
			//shield opacity = 33%
			_shieldOpacity.color = new Color(1f, 1f, 1f, .3f);
		}
		else
		{
			_isShieldActive = false;
			_shieldVisualizer.SetActive(false);
		}

	}
	public void HealthPowerupCollected()
	{
		if (_health < 3)
		{
			_health++;
		}
	}
	public void AmmoCollected()
	{
		ammoCount += 15;
	}
	public void GrenadeCollected()
	{
		_haveGrenade = true;
		_uiManager.GrenadeCollected();
	}
	void GrenadeFire()
	{
		if (_haveGrenade == true && Input.GetKeyDown(KeyCode.G) == true)
		{
			var enemies = GameObject.FindGameObjectsWithTag("Enemy");
			for (int i = 0; i < enemies.Length; i++)
			{
				Destroy(enemies[i]);
			}
			_haveGrenade = false;
			_uiManager.GrenadeUsed();
		}
	}
}