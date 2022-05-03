using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Warehouse : BuildingBase
{
    private static ResourceTypesInventory _warehouseInvetory;
    public static ResourceTypesInventory warehouseInvetory {
        get 
        {
            // Initializes united inventory
            if(_warehouseInvetory == null){
                _warehouseInvetory = new ResourceTypesInventory();
                warehouseInvetory.AddAmmountOrAddNewItem(ResourceTypes.WOOD, 1500);
                warehouseInvetory.AddAmmountOrAddNewItem(ResourceTypes.STONE, 800);
                warehouseInvetory.AddAmmountOrAddNewItem(ResourceTypes.FOOD, 1800);
            }
            
            return _warehouseInvetory;
        }
    }
    
    protected override void Awake()
    {
        base.Awake();   
        MaxAgantCapacity = 10; 

        // Prirazeni spolecneho uloziste pro skladiste
        Resources = warehouseInvetory;
    }
}
