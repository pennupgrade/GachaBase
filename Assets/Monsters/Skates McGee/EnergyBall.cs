using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
	
	private float speed;
    
    public void SetSpeed(float newSpeed)
    {
		speed = newSpeed;
	}

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
        
        if (this.transform.position.y <= -4.0) 
        {
			Destroy(gameObject);
		}
    }
}
