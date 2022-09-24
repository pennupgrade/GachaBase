using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkatesMcGee : MonoBehaviour
{

	private float speed = 3.0f;
	public GameObject shooter;
	
	private SpriteRenderer spriteRenderer;
	
	public Sprite idle;
	public Sprite movingLeft;
	public Sprite movingRight;
	public Sprite deadLeft;
	public Sprite deadRight;
	
	private bool isDead = false;
	private float timePassed = 0;
	private float timer = 5;
	
    // Start is called before the first frame update
    void Start()
    {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
		if (!isDead) 
		{
			UpdateTimer();
			if (Input.GetKey(KeyCode.A)) 
			{
				spriteRenderer.sprite = movingLeft;
				this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
				if (this.transform.position.x <= -8.00) 
				{
					this.transform.position = new Vector3(-8.00f, this.transform.position.y, this.transform.position.z);
				}
			}
			if (Input.GetKey(KeyCode.D)) 
			{
				spriteRenderer.sprite = movingRight;
				this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
				if (this.transform.position.x >= 8.00) 
				{
					this.transform.position = new Vector3(8.00f, this.transform.position.y, this.transform.position.z);
				}
			}
			if (!Input.anyKey) 
			{
				spriteRenderer.sprite = idle;
			}
		}
    }
    
    void UpdateTimer() 
    {
		timePassed += Time.deltaTime;
		timer -= Time.deltaTime;
		
		if (timer <= 0) 
		{
			GenerateCurrency();
			timer = 5;
		}
	}
    
    void GenerateCurrency() 
    {
		int amtGained = (int) Mathf.Floor(timePassed/5);
		CurrencyManager.Instance.Currency += amtGained;
	}
    
    void OnTriggerEnter2D(Collider2D col) 
    {
		isDead = true;
		
		if (spriteRenderer.sprite == movingRight) 
		{
			spriteRenderer.sprite = deadRight;
		} else if (spriteRenderer.sprite == movingLeft) 
		{
			spriteRenderer.sprite = deadLeft;
		} else 
		{
			spriteRenderer.sprite = deadRight;
		}
		
		shooter.GetComponent<Shooter>().GameOver();
		shooter.GetComponent<ShootEnergyBall>().StopShooting();
		Destroy(col.gameObject);
	}
}
