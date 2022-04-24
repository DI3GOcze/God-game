using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] int numberOfWoodCuters = 10;
    [SerializeField] int numberOfStoneDiggers = 10;
    [SerializeField] int numberOfFoodGatheres = 10;
    [SerializeField] GameObject VillagerPrefab;
    [SerializeField] GameObject Parent;

    [SerializeField] LayerMask layer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnVillagers());
    }

    IEnumerator SpawnVillagers()
    {
        for (int i = 0; i < numberOfWoodCuters; i++)
        {
            GameObject woodCutter = Instantiate(VillagerPrefab, VillagerPrefab.transform.position + GenerateRandomOffset(50, 50), VillagerPrefab.transform.rotation, Parent.transform);
            woodCutter.GetComponent<Grown>().profession = Professions.Woodcuter;
        }

        for (int i = 0; i < numberOfStoneDiggers; i++)
        {
            GameObject miner = Instantiate(VillagerPrefab, VillagerPrefab.transform.position + GenerateRandomOffset(50, 50), VillagerPrefab.transform.rotation, Parent.transform);
            miner.GetComponent<Grown>().profession = Professions.Miner;
        }

        for (int i = 0; i < numberOfFoodGatheres; i++)
        {
            GameObject foodGatherer = Instantiate(VillagerPrefab, VillagerPrefab.transform.position + GenerateRandomOffset(50, 50), VillagerPrefab.transform.rotation, Parent.transform);
            foodGatherer.GetComponent<Grown>().profession = Professions.FoodGatherer;
        }
        yield return null;
    }

    Vector3 GenerateRandomOffset(float x, float z)
    {
        // Doesnt work with height higher than 1000
        RaycastHit hit;
        float randX = Random.Range(-x, x) + transform.position.x;
        float randZ = Random.Range(-z, z) + transform.position.z;

        Ray ray = new Ray(new Vector3(randX, 100, randZ), Vector3.down);
        Physics.Raycast(ray, out hit, Mathf.Infinity, layer);
        
        return hit.point;
    }
}
