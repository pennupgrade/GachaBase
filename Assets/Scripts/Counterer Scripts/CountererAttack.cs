using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountererAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private EnemyAttack ea;

    private bool attacking = false;

    private float timeToAttack = 0.25f;
    private float timer = 0f;
    public float coolDown;
    private float coolDownTimer;

    public static bool changedColor;

    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        attackArea.SetActive(attacking);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && coolDownTimer >= coolDown)
        {
            if(changedColor)
            {
                Attack();
                changedColor = false;
            }
            coolDownTimer = 0;
        }

        coolDownTimer += Time.deltaTime;

        if(attacking)
        {
            timer += Time.deltaTime;

            if(timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
    }

    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
    }

    public static void changeColor(bool change)
    {
        changedColor = change;
    }
}
