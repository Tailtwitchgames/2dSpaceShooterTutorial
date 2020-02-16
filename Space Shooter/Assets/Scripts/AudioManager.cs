using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioClip _laserFireClip;
	[SerializeField] private AudioClip _powerupPickupClip;
	[SerializeField] private AudioClip _explosionClip;
	[SerializeField] private AudioSource _laserFireSource;
	[SerializeField] private AudioSource _powerupPickupSource;
	[SerializeField] private AudioSource _explosionSource;
	[SerializeField] private AudioClip _backgroundMusicClip;
	[SerializeField] private AudioSource _backgroundMusicSource;
	[SerializeField] private AudioClip _emptyAmmoClip;
	[SerializeField] private AudioSource _emptyAmmoSource;
    // Start is called before the first frame update
    void Start()
    {
		_backgroundMusicSource.clip = _backgroundMusicClip;
		_backgroundMusicSource.loop = true;
		_backgroundMusicSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
    }
	public void ExplosionSound()
	{
		_explosionSource.clip = _explosionClip;
		_explosionSource.Play();
	}
	public void PowerupSound()
	{
		_powerupPickupSource.clip = _powerupPickupClip;
		_powerupPickupSource.Play();
	}
	public void LaserFireSound()
	{
		_laserFireSource.clip = _laserFireClip;
		_laserFireSource.Play();
	}
	public void EmptyAmmoSound()
	{
		_emptyAmmoSource.clip = _emptyAmmoClip;
		_emptyAmmoSource.Play();
	}
}
