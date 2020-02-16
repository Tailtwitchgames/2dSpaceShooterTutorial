using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
	//speed of 8m/sec
	private float _speed = 8f;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//move laser up 
		transform.Translate(Vector3.up * _speed * Time.deltaTime);

		if (transform.position.y > 8)
		{
			//Debug.Log("Laser Detection Position.");
			if (transform.parent != null)
			{
				//Debug.Log("Game says there was a parent.");
				Destroy(transform.parent.gameObject);
			}
			else
			{
				//Debug.Log("Destroy logic active.");
				Destroy(this.gameObject);
			}
			//Debug.Log("Past Destroy Logic Section.");
		}
    }
}
