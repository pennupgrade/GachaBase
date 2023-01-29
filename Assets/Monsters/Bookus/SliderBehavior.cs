using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehavior : MonoBehaviour
{
    public Slider slider;
    public TimerBehavior timer;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        float current = timer.getTimer();
        if (current <= 3f) { 
            slider.value = current;
        } else
        {
            slider.value = 0f;
        }
    }
}
