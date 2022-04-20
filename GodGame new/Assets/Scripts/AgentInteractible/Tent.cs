using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tent : AgentInteractibleBase
{
    protected override void Awake()
    {
        base.Awake();   
        MaxAgantCapacity = 4; 
    }
}
