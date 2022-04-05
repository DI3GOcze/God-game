using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Warehouse : AgentInteractibleBase
{
    public static ResourceTypesInventory warehouseInvetory;

    protected override void Start() {
        base.Start();
        
        // Skladiste maji spolecne uloziste
        // Pokud toto uloziste jeste neni vytvoreno, vytvor ho
        if (warehouseInvetory == null)
            warehouseInvetory = new ResourceTypesInventory();

        // Prirazeni spolecneho uloziste pro skladiste
        Resources = warehouseInvetory;
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
