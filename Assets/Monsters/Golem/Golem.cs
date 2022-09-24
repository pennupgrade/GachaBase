using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    public float THRESHOLD;
    public float ON_CLICKED_SCALE;
    public float PARTICLE_TIME_LENGTH;
    public float AWARDED_CURRENCY;

    private ParticleSystem particles;
    private int counter;
    private Vector3 initialScale;
    private float clickedTime = 0;

    public void AwardCurrency()
    {
        // This line adds 1 to our currency.
        CurrencyManager.Instance.Currency += AWARDED_CURRENCY;
    }

    // Start is called before the first frame update       
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        counter = 0;
        initialScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {            
            // Checking if the click will hit this monster's box collider 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null)
            {
                counter++;
                clickedTime = Time.time;
                if (counter == THRESHOLD)
                {
                    //Explode;
                    particles.Play();

                    //Give out coins
                    AwardCurrency();

                    //Reinstantiate object
                    gameObject.transform.localScale = initialScale;

                    //Reset counter
                    counter = 0;
                }
                else
                {
                    //Scale up the object
                    gameObject.transform.localScale += new Vector3(ON_CLICKED_SCALE, ON_CLICKED_SCALE, ON_CLICKED_SCALE);
                }
            }
        }

        if (Time.time - clickedTime >= PARTICLE_TIME_LENGTH && particles.isPlaying)
        {
            particles.Stop();
        }
    }
}
