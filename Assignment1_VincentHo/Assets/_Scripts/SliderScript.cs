using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slider;

    public void SetMaxVelocity(float velocity)
    {
        slider.maxValue = velocity;
    }

    public void displayVelocity(float vel)
    {
        slider.value = vel;
    }

}
