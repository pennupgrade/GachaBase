using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class EnemyAttack : MonoBehaviour
{
    public GameObject counterer;
    public CountererAttack ct;
    private GameObject attackArea = default;

    private bool attacking = false;

    private SpriteRenderer rend;

    public Color newColor;

    public float coolDown = 10f;
    public float coolDownTimer = 0;
    public float changeColor = 1f;

    public bool changedColor;
    public bool collided;

    // Start is called before the first frame update
    void Start()
    {
        attackArea.SetActive(false);
        changedColor = false;
        collided = false;
    }

    private void Update()
    {
        coolDownTimer += Time.deltaTime;

        if(!collided)
        {
            coolDownTimer = 0;
        }

        if(coolDownTimer >= changeColor)
        {
            rend = GetComponent<SpriteRenderer>();
            rend.color = newColor;
            changedColor = true;
            CountererAttack.changeColor(changedColor);
            if (coolDownTimer >= coolDown)
            {
                EnemyHealth health = counterer.GetComponent<EnemyHealth>();
                health.Damage(1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Counterer"))
        {
            collided = true;
        }
    }
}
