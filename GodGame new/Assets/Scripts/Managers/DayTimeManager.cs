using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum TimesOfDay
{
    Morning,
    Afternoon,
    Evening,
    Night
}
public class DayTimeManager : MonoBehaviour
{
    /// <summary>
    /// Time scaled between 0 and 1
    /// </summary>
    public float scaledTime;

    [Range(0f,1f)]
    public float startingTime = 0;
        
    /// <summary>
    /// Length of day in seconds
    /// </summary>
    [SerializeField]
    private float _dayLength = 10;
    public float dayLength 
    {
        get { return _dayLength; } 
        private set { _dayLength = value; }
    }
    public int hours { get; private set; }
    public int minutes 
    {
        get { return (int)((scaledTime * 1440) % 60); }
    }
    
    public Vector3 noon;

    // Servers for skybox change
    public Light fakeSun;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntesity;

    public AnimationCurve lightIntesityMultiplier;
    public AnimationCurve reflectionIntesityMultiplier;
    public TextMeshProUGUI timeText;

    private void Start() {
        scaledTime = startingTime;
    }

    public void IncreaseTimeByHours(int hours){
        float normalizedHours = (float)hours / 24f;
        scaledTime += normalizedHours;
        if(scaledTime >= 1)
            scaledTime -= 1f;
    }
    
    // Update is called once per frame
    void Update()
    {
        scaledTime += Time.deltaTime / _dayLength;
        if(scaledTime >= 1)
            scaledTime = 0f;

        // Rotation 
        fakeSun.transform.eulerAngles = (scaledTime - 0.25f) * noon * 4.0f;
        
        // Intensity 
        sun.intensity = sunIntesity.Evaluate(scaledTime);

        // Colors
        sun.color = sunColor.Evaluate(scaledTime);

        RenderSettings.ambientIntensity = lightIntesityMultiplier.Evaluate(scaledTime);
        RenderSettings.reflectionIntensity = reflectionIntesityMultiplier.Evaluate(scaledTime);


        hours = (int)(scaledTime*24);
        
        if(hours >= 6 && hours <= 9){
            World.Instance.ChangeTimeOfDay(TimesOfDay.Morning);
        } else if (hours >= 10 && hours <= 17){
            World.Instance.ChangeTimeOfDay(TimesOfDay.Afternoon);
        } else if (hours >= 18 && hours <= 22){
            World.Instance.ChangeTimeOfDay(TimesOfDay.Evening);
        } else if (hours >= 23 && hours <= 24 || hours >= 0 && hours <= 5){
            World.Instance.ChangeTimeOfDay(TimesOfDay.Night);
        }
            

        timeText.text = $"{(TimesOfDay)World.Instance.timeOfDay} \n {hours:00}:{minutes:00}";
    }
}
