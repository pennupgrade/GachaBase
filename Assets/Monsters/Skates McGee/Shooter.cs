using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
	private float speed = 2.0f;
	private int dir = 0; // 0 = right, 1 = left
	
	private static bool gameOver;
    
    void Start() 
    {
		gameOver = false;
	}

    // Update is called once per frame
    void Update()
    {
		if (!gameOver) 
		{
			if (this.transform.position.x >= 8.00) 
			{
				dir = 1;
			}
			if (this.transform.position.x <= -8.00) 
			{
				dir = 0;
			}
			
			switch (dir) 
			{
				case 0:
					this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
					break;
				case 1:
					this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
					break;
			}
		}
    }
    
    public void AddSpeed(float increment)
    {
		speed += increment;
	}
    
    public void GameOver() 
    {
		gameOver = true;
	}
}
