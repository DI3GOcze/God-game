using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderColorChange : MonoBehaviour
{
    public Slider Slider; //connected the slider
    public Image SliderFill; //connected the Image Fill from the slider
    public Color LowValueColor = Color.green;
    public Color HighValueColor = Color.red;
    private void Update(){
        if(gameObject.activeSelf)
            SliderFill.color = Color.Lerp(LowValueColor, HighValueColor, Slider.value / Slider.maxValue);
    }   
}
