using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatManager : MonoBehaviour
{
    public float maxStamina = 100;
    public float staminaRegenerationRate = 5;
    public float stamina;
    public Slider slider;
    public static PlayerStatManager instance { get; private set; }

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
    }

    public void IncreaseStamina(float amount)
    {
        stamina += amount;
        if(stamina > maxStamina) {
            stamina = maxStamina;
        }
    }

    public void DecreseStamina(float amount)
    {
        stamina -= amount;
        if(stamina < 0) {
            stamina = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseStamina(Time.deltaTime * staminaRegenerationRate);        


        if(slider.gameObject.activeInHierarchy) {
            slider.value = stamina / maxStamina;
        }
    }
}
