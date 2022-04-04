using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Warehouse : AgentInteractibleBase
{
    protected override void Start() {
        base.Start();
    }
    protected override void Awake()
    {
        base.Awake();   
        MaxAgantCapacity = 10; 
    }

    private void Update() {
        UpdateGUI();
    }

    protected override void OnMouseExit()
    {
        // Dont deactivate GUI
    }
}
