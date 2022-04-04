using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerCreateManager : MonoBehaviour
{
    public GameObject villagerPrefab;
    public Transform spawnPoint;
    public Transform villagerParent;

    public void SpawnVillager()
    {
        Instantiate(villagerPrefab, spawnPoint.position, villagerPrefab.transform.rotation, villagerParent);
    }
}
