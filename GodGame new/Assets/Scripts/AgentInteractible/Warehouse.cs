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
            if(_warehouseInvetory == null){
                _warehouseInvetory = new ResourceTypesInventory();
                warehouseInvetory.AddAmmountOrAddNewItem(ResourceTypes.WOOD, 200);
                warehouseInvetory.AddAmmountOrAddNewItem(ResourceTypes.STONE, 100);
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
