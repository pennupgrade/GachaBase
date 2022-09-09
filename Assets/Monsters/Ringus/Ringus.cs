using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A simple test monster that will give you 1 currency when you click on it.
public class Ringus : MonoBehaviour
{

    private AudioSource sfx;
    private ParticleSystem particles;
    private float clickedTime = 0;

    private Vector3 initialScale;
    private Vector3 initialPosition;
    private Quaternion initialRotation;


    public float PARTICLE_TIME_LENGTH;
    public float ON_CLICKED_SCALE;
    public float ON_CLICKED_POSITION_RANGE;
    public float ON_CLICKED_ROTATION_RANGE;



    public void GenerateCurrency()
    {
        // This line adds 1 to our currency.
        CurrencyManager.Instance.Currency += 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<AudioSource>();
        particles = GetComponent<ParticleSystem>();
        initialScale = gameObject.transform.localScale;
        initialPosition = gameObject.transform.position;
        initialRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Checking if the click will hit this monster's box collider 

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.Log(ray);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null)
            {

                clickedTime = Time.time;
                particles.Play();

                sfx.Play(0);
                Debug.Log("Clicked");
                GenerateCurrency();

                gameObject.transform.localScale = new Vector3(ON_CLICKED_SCALE, ON_CLICKED_SCALE, ON_CLICKED_SCALE);
                gameObject.transform.position = new Vector3(ON_CLICKED_POSITION_RANGE * Random.Range(-1f,1f), ON_CLICKED_POSITION_RANGE * Random.Range(-1f, 1f), 0);
                gameObject.transform.rotation = Quaternion.Euler(ON_CLICKED_ROTATION_RANGE * Random.Range(-1f, 1f), ON_CLICKED_ROTATION_RANGE * Random.Range(-1f, 1f), ON_CLICKED_ROTATION_RANGE * Random.Range(-1f, 1f));

            }
        }

        /*
        if (Vector3.Distance(gameObject.transform.localScale,initialScale) > 0.02f)
        {
            gameObject.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        }
        */
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, initialScale, 0.01f);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, initialPosition, 0.05f);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, initialRotation, 0.05f);


        if (Time.time - clickedTime >= PARTICLE_TIME_LENGTH && particles.isPlaying)
        {
            particles.Stop();
        }

    }
}
