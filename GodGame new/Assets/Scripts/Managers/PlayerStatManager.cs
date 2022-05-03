using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatManager : MonoBehaviour
{
    public float maxMana = 100;
    public float manaRegenerationRate = 5;
    public float mana;
    public Slider slider;
    public static PlayerStatManager instance { get; private set; }

    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mana = maxMana;
    }

    /// <summary>
    /// Increses players mana by amount  
    /// </summary>
    /// <param name="amount">Incese mana by amount</param>
    public void IncreaseMana(float amount)
    {
        mana += amount;
        if(mana > maxMana) {
            mana = maxMana;
        }
    }

    /// <summary>
    /// Decreses players mana by amount  
    /// </summary>
    /// <param name="amount">Decrese mana by amount</param>
    public void DecreseMana(float amount)
    {
        mana -= amount;
        if(mana < 0) {
            mana = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Player regenerates mana with time
        IncreaseMana(Time.deltaTime * manaRegenerationRate);        
    
        // Update player GUI slider
        if(slider.gameObject.activeInHierarchy) {
            slider.value = mana / maxMana;
        }
    }
}
