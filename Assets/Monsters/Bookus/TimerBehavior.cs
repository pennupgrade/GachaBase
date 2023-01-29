using System;
using UnityEngine;
using TMPro;

public class TimerBehavior : MonoBehaviour
{
    public TMP_Text remaining;
    private float timer;

    public void reset()
    {
        timer = 3f;
    }

    public float getTimer()
    {
        return timer;
    }

    void Start()
    {
        timer = 3f;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 2f)
        {
            remaining.text = "3";
        } else if (timer > 1f)
        {
            remaining.text = "2";
        } else
        {
            remaining.text = "1";
        }
    }
}
