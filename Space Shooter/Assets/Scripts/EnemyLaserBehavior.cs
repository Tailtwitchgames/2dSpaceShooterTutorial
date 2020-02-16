using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserBehavior : MonoBehaviour
{
	private float _enemyLaserSpeed = 5f;
	private Player _player;
    // Start is called before the first frame update
    void Start()
    {
		_player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
		transform.Translate(Vector3.down * Time.deltaTime * _enemyLaserSpeed);
		if (transform.position.y <= -6f)
		{
			Destroy(this.gameObject);
		}
    }
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			_player.Health();
		}

	}
}
