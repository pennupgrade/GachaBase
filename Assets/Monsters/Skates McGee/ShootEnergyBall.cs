using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnergyBall : MonoBehaviour
{
	
	public GameObject energyBall;
	public GameObject shooter;
	
	private float timer;
	private float minTime = 3.0f;
	private float maxTime = 4.0f;
	private float ballSpeed = 1.0f;
	
	private static bool keepShooting;
	
    // Start is called before the first frame update
    void Start()
    {
		keepShooting = true;
        timer = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {	
		if (keepShooting) 
		{
			timer -= Time.deltaTime;
			
			if (timer <= 0) 
			{
				GameObject energyBallClone = Instantiate(energyBall) as GameObject;
				energyBallClone.transform.position = this.transform.position;
				energyBallClone.GetComponent<EnergyBall>().SetSpeed(ballSpeed);
				
				timer = Random.Range(minTime, maxTime);

				if (minTime > 0.5f) 
				{
					minTime -= 0.1f;
					maxTime -= 0.1f;
					ballSpeed += 0.1f;
				}
				
				shooter.GetComponent<Shooter>().AddSpeed(0.1f);
			}
			
		}
    }
    
    public void StopShooting() 
    {
		keepShooting = false;
	}
}
