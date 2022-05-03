using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateText : MonoBehaviour
{
    public Warehouse warehouse;
    public TextMeshProUGUI wood;
    public TextMeshProUGUI stone;
    public TextMeshProUGUI food;
    
    private ResourceTypesInventory inventory;

    private void Start() {
        inventory = Warehouse.warehouseInvetory;
    }

    private void Update() {
        
        string woodNum = inventory.ItemAmount(ResourceTypes.WOOD).ToString();
        string stoneNum = inventory.ItemAmount(ResourceTypes.STONE).ToString();
        string foodNum = inventory.ItemAmount(ResourceTypes.FOOD).ToString();
        
        if(wood != null)
            wood.text = "Wood: " + woodNum;

        if(stone != null)
            stone.text = "Stone: " + stoneNum;

        if(food != null)
            food.text = "Food: " + foodNum;
    }    
    
}
