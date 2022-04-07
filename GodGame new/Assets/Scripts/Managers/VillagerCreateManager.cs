using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillagerCreateManager : MonoBehaviour
{
    public GameObject villagerPrefab;
    public Transform spawnPoint;
    public Transform villagerParent;

    public int woodRequested;
    public int stoneRequested;
    public int foodRequested;

    private Button button;

    private void Start() {
        button = GetComponent<Button>();
    }

    private void Update() {
        if(button != null)
        {
            var inventory = Warehouse.warehouseInvetory;
            var woodAmount = inventory.ItemAmount(ResourceTypes.WOOD);
            var stoneAmount = inventory.ItemAmount(ResourceTypes.STONE);
            var foodAmount = inventory.ItemAmount(ResourceTypes.FOOD);

            // If polayer has enough resources
            if(woodAmount >= woodRequested && stoneAmount >= stoneRequested && foodAmount >= foodRequested) {
                button.interactable = true;
            } 
            else {
                button.interactable = false;
            }
        }
    }

    public void SpawnVillager()
    {
        var inventory = Warehouse.warehouseInvetory;

        inventory.SeizeItem(ResourceTypes.WOOD, woodRequested);
        inventory.SeizeItem(ResourceTypes.STONE, stoneRequested);
        inventory.SeizeItem(ResourceTypes.FOOD, foodRequested);

        Instantiate(villagerPrefab, spawnPoint.position, villagerPrefab.transform.rotation, villagerParent);  
    }
}
