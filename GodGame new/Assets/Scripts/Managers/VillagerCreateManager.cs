using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerCreateManager : MonoBehaviour
{
    public GameObject villagerPrefab;
    public Transform spawnPoint;
    public Transform villagerParent;

    public int woodRequested;
    public int stoneRequested;
    public int foodRequested;

    public void SpawnVillager()
    {
        var inventory = Warehouse.warehouseInvetory;

        var woodAmount = inventory.ItemAmount(ResourceTypes.WOOD);
        var stoneAmount = inventory.ItemAmount(ResourceTypes.STONE);
        var foodAmount = inventory.ItemAmount(ResourceTypes.FOOD);
        
        if(woodAmount >= woodRequested && stoneAmount >= stoneRequested && foodAmount >= foodRequested)
        {
            inventory.SeizeItem(ResourceTypes.WOOD, woodRequested);
            inventory.SeizeItem(ResourceTypes.STONE, stoneRequested);
            inventory.SeizeItem(ResourceTypes.FOOD, foodRequested);

            Instantiate(villagerPrefab, spawnPoint.position, villagerPrefab.transform.rotation, villagerParent);
        }

        
    }
}
