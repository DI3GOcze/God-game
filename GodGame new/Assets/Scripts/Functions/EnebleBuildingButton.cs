using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.UI;

public class EnebleBuildingButton : MonoBehaviour
{
    public BuildingScriptableObject building;

    private Button _button;

    private void Start() {
        _button = GetComponent<Button>();
    }

    private void Update() {        
        var inventory = Warehouse.warehouseInvetory;

        var woodAmount = inventory.ItemAmount(ResourceTypes.WOOD);
        var stoneAmount = inventory.ItemAmount(ResourceTypes.STONE);

        // If not enough resources
        if(woodAmount < building.woodCost || stoneAmount < building.stoneCost) 
            _button.interactable = false;
        else
            _button.interactable = true;
    }
}
