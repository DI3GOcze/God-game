using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSliderValue : MonoBehaviour
{
    public Slider slider;
    public Person person;

    private void Update() {
        slider.value = person.HungerNormalized;
    }

}
