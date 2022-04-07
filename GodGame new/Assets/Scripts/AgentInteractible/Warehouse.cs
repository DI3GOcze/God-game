using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Warehouse : AgentInteractibleBase
{
    public static ResourceTypesInventory warehouseInvetory;
    protected override void Awake()
    {
        base.Awake();   
        MaxAgantCapacity = 10; 

        // Skladiste maji spolecne uloziste
        // Pokud toto uloziste jeste neni vytvoreno, vytvor ho
        if (warehouseInvetory == null)
            warehouseInvetory = new ResourceTypesInventory();

        // Prirazeni spolecneho uloziste pro skladiste
        Resources = warehouseInvetory;
    }
}
