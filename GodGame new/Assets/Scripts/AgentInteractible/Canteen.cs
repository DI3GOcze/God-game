using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Canteen : BuildingBase
{
    protected override void Start() {
        base.Start();

        // Canteen uses united inventory
        Resources = Warehouse.warehouseInvetory;
    }
    protected override void Awake()
    {
        base.Awake();   
        MaxAgantCapacity = 2; 
    }
}
