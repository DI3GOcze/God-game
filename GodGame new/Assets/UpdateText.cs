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

    private void Start() {
        if (warehouse == null)
            warehouse = GameObject.Find("Warehouse").GetComponent<Warehouse>();
    }

    private void Update() {
        if(warehouse == null)
            return;
        
        string woodNum;
        if(warehouse.Resources.Items.ContainsKey(ResourceTypes.WOOD))
            woodNum = warehouse.Resources.Items[ResourceTypes.WOOD].ToString();
        else
            woodNum = "0";

        string stoneNum;
        if(warehouse.Resources.Items.ContainsKey(ResourceTypes.STONE))
            stoneNum = warehouse.Resources.Items[ResourceTypes.STONE].ToString();
        else
            stoneNum = "0";

        string foodNum = "0";
        if(warehouse.Resources.Items.ContainsKey(ResourceTypes.FOOD))
            foodNum = warehouse.Resources.Items[ResourceTypes.FOOD].ToString();
        else
            foodNum = "0";

        if(wood != null)
            wood.text = "Wood: " + woodNum;

        if(stone != null)
            stone.text = "Stone: " + stoneNum;

        if(food != null)
            food.text = "Food: " + foodNum;
    }    
    
}
