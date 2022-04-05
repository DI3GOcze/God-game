using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Person has its home, parents?, hunger
public class Person : GAgentBase
{
    public float maxHunger = 100f;
    public float HungerIncreasePerSecond = 1f;
    [SerializeField]
    /// <summary>0 = not hungry, maxHunger => starving</summary>

    protected float hunger = 0;
    /// <summary>Value from 0 - 1 (0 => not hungry, 1 => hungry)</summary>
    public float HungerNormalized => Mathf.Clamp(hunger / maxHunger, 0, 1);
    // ...

    public void ClearHunger()
    {
        hunger = 0f;
    }

    protected override void Start()
    {
        base.Start();
        InvokeRepeating("IncreseHunger", 0f, 1f);
    }

    protected void IncreseHunger()
    {
        // If person starves, he dies
        if ((hunger += HungerIncreasePerSecond * Random.Range(0.8f, 1.2f)) >= maxHunger)
        {
            hunger = maxHunger;
            Die();
        }
    }
}
