using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Canteen : AgentInteractibleBase
{
    protected override void Start() {
        base.Start();

        // Jidelny maji spolecne uloziste se skladistem
        // Pokud toto uloziste jeste neni vytvoreno, vytvor ho
        if (Warehouse.warehouseInvetory == null)
            Warehouse.warehouseInvetory = new ResourceTypesInventory();

        // Prirazeni spolecneho uloziste pro jidelny
        Resources = Warehouse.warehouseInvetory;
    }
    protected override void Awake()
    {
        base.Awake();   
        MaxAgantCapacity = 2; 
    }

    private void Update() {
        UpdateGUI();
    }

    protected override void OnMouseExit()
    {
        // Dont deactivate GUI
    }
}
