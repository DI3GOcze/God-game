using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Building", order = 2)]
public class BuildingScriptableObject : ScriptableObject
{
    public GameObject buildingPrefab;
    public GameObject buildingGhost;
    public int woodCost;
    public int stoneCost;
}
