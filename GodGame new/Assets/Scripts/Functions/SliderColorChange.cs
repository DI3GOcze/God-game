using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Changes dynamicaly color of slider based on his value
/// </summary>
public class SliderColorChange : MonoBehaviour
{
    public Slider Slider; 
    public Image SliderFill;
    public Color LowValueColor = Color.green;
    public Color HighValueColor = Color.red;
    private void Update(){
        if(gameObject.activeSelf)
            SliderFill.color = Color.Lerp(LowValueColor, HighValueColor, Slider.value / Slider.maxValue);
    }   
}
