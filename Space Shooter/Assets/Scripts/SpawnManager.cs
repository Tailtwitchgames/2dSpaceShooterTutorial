using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private GameObject _enemyPrefab;
	[SerializeField] private GameObject _tripleShotPrefab;
	[SerializeField] private GameObject _speedUpPrefab;
	[SerializeField] private GameObject _shieldPowerUpPrefab;
	private bool _continueSpawn = true;
	[SerializeField] private GameObject _asteroidPrefab;
	[SerializeField] private bool _isAsteroidDead;
	[SerializeField] private GameObject _healthPrefab;
	[SerializeField] private GameObject _ammoPrefab;
	[SerializeField] private GameObject _grenadePrefab;
	private Vector3 _spawnPosition;

	// Start is called before the first frame update
	void Start()
    {
		StartCoroutine(SpawnAsteroid());
		_spawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
	}

    // Update is called once per frame
    void Update()
    {
    }

	public void AsteroidDestroyed()
	{
		_isAsteroidDead = true;
		StartCoroutine(SpawnEnemyRoutine());
		StartCoroutine(SpawnTripleShotRoutine());
		StartCoroutine(SpawnSpeedUpRoutine());
		StartCoroutine(SpawnShieldUpRoutine());
		StartCoroutine(SpawnGrenadeRoutine());
	}
	IEnumerator SpawnGrenadeRoutine()
	{
		yield return new WaitForSeconds(Random.Range(0f, 50f));
		Instantiate(_ammoPrefab, _spawnPosition, Quaternion.identity);
		yield return new WaitForSeconds(5f);
	}
	IEnumerator SpawnAsteroid()
	{
		while (_continueSpawn == true)
		{
			Vector3 _asteroidSpawnPosition = new Vector3(Random.Range(-8f, 8f), Random.Range(3f, 7f), 0);
			Instantiate(_asteroidPrefab, _asteroidSpawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(.1f);
			break;
		}
	}
	IEnumerator SpawnAmmoRoutine()
	{
		while (_continueSpawn == true && _isAsteroidDead == true)
		{
			yield return new WaitForSeconds(Random.Range(0f, 20f));
			Instantiate(_ammoPrefab, _spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(5f);
		}
	}
	IEnumerator SpawnEnemyRoutine()
	{
		//Debug.Log("Spawn Enemy Routine Before If");
		while (_continueSpawn == true && _isAsteroidDead == true)
		{
			//Debug.Log("EnemySpawnRoutine Logic Active.");
			Instantiate(_enemyPrefab, _spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(5f);
		}
	}
	IEnumerator SpawnHealthRoutine()
	{
		yield return new WaitForSeconds(Random.Range(0f, 50f));
		Instantiate(_healthPrefab, _spawnPosition, Quaternion.identity);
		yield return new WaitForSeconds(5f);
	}
	IEnumerator SpawnTripleShotRoutine()
	{
		while (_continueSpawn == true && _isAsteroidDead == true)
		{
			yield return new WaitForSeconds(Random.Range(0f, 30f));
			Instantiate(_tripleShotPrefab, _spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(30f);
		}
	}
	IEnumerator SpawnSpeedUpRoutine()
	{
		while (_continueSpawn == true && _isAsteroidDead == true)
		{
			yield return new WaitForSeconds(Random.Range(0f, 30f));
			Instantiate(_speedUpPrefab, _spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(30f);
		}
	}
	IEnumerator SpawnShieldUpRoutine()
	{
		while (_continueSpawn == true && _isAsteroidDead == true)
		{
			yield return new WaitForSeconds(Random.Range(0f, 30f));
			Instantiate(_shieldPowerUpPrefab, _spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(30f);
		}
	}
	public void OnPlayerDeath()
	{
		//Debug.Log("Stopping new spawn.");
		_continueSpawn = false;
	}
}
