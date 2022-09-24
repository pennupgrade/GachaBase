using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour

{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void danceLeft() {

        anim.Play("DragonLeft");
        //anim.SetBool("Left", true);
    }

    public void danceRight() {
        anim.Play("DragonRight");
        //anim.SetBool("Right", true);
    }

    public void danceUp()
    {
        anim.Play("DragonUp");
        //anim.SetBool("Up", true);
    }

    public void danceDown()
    {
        anim.Play("DragonDown");
        //anim.SetBool("Down", true);
    }

    public void stopMove()
    {
        anim.SetBool("Up", false);
        anim.SetBool("Left", false);
        anim.SetBool("Down", false);
        anim.SetBool("Right", false);
    }
}
