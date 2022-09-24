using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A simple test monster that will give you 1 currency when you click on it.


// Jump (change y position)
// Evolve
// Change amount of money gained when evolution happens

public class Geoff : MonoBehaviour
{
    private int evolution_stage = 1;
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

    private SpriteRenderer spriteRenderer;

    public Sprite geoff;
    public Sprite geoffry;
    public Sprite geofferson;



    public void GenerateCurrency()
    {
        // This line adds 1 to our currency.
        CurrencyManager.Instance.Currency = CurrencyManager.Instance.Currency + evolution_stage;
    }

    public void UpdateStage() {
        // up
        if(evolution_stage < 3) {
            if(CurrencyManager.Instance.Currency >= 20 && evolution_stage == 1) {
                CurrencyManager.Instance.Currency -= 20;
                evolution_stage = 2;
            } else if(CurrencyManager.Instance.Currency >= 40 && evolution_stage == 2) {
                CurrencyManager.Instance.Currency -= 40;
                evolution_stage = 3;
            }
        }

        // evolution stage sprite change
        switch (evolution_stage) {
            case 1: //stage1
                spriteRenderer.sprite = geoff;
                break;
            case 2: // stage2
                spriteRenderer.sprite = geoffry;
                break;
            case 3: // final stage
                spriteRenderer.sprite = geofferson;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
            UpdateStage();
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null)
            {
                GenerateCurrency();
                clickedTime = Time.time;
                particles.Play();

                sfx.Play(0);
                Debug.Log("Clicked");
                

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
