using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotPowerup : MonoBehaviour
{
	[SerializeField] private float _speed = 3.0f;
	//Remember that powerUpSelector is set by putting this script on the various powerup prefabs and then throwing a number into the field per prefab
	//0 = triple shot, 1 = speed up, 2 = shields, 3 = Ammo, 4 = Health, 5 = Grenade
	[SerializeField] private int powerUpSelector;
	private AudioManager _audioManager;


    // Start is called before the first frame update
    void Start()
    {
		_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
		//move down at speed 3m / sec
		transform.Translate(Vector3.down * Time.deltaTime * _speed);
		//When this leaves screen, destroy this
		if (transform.position.y <= -4f)
		{
			Destroy(this.gameObject);
		}
	
    }
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Player player = other.transform.GetComponent<Player>();
			_audioManager.PowerupSound();
			if (player != null)
			{
				switch(powerUpSelector)
				{
					case 0:
						//Debug.Log("Triple Shot Collected.");
						player.TripleShotActivate();
						Destroy(this.gameObject);
						break;
					case 1:
						//Debug.Log("Speed Up Collected");
						player.SpeedUp();
						Destroy(this.gameObject);
						break;
					case 2:
						//Debug.Log("Shield collected.");
						player.ShieldsActive();
						break;
					case 3:
						//Debug.Log("Ammo Powerup Collected");
						player.AmmoCollected();
						break;
					case 4:
						//Debug.Log("Health Powerup Collected");
						player.HealthPowerupCollected();
						break;
					case 5:
						//Debug.Log("Grenade Powerup Collected");
						player.GrenadeCollected();
						break;
				}

				Destroy(this.gameObject);
			}
		}
	}
	//OnTriggerCollision w/ player
	//componentget from other, run function to alter variable
	//note, need to create that function in player
	//destroyself when collected

}
