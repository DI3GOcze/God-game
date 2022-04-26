using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public int Trees;
    public int Stones;
    public int Warehouses;
    public float TimeScale = 1f;

    void Start()
    {
        LimitFPS();
    }

    void LimitFPS()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 90;
    }

    private void Update() 
    {
        Time.timeScale = TimeScale;
        Trees = World.Instance.trees.Count;
        Stones = World.Instance.stones.Count;
        Warehouses = World.Instance.warehouses.Count;
    }

    public void Armagedon()
    {
        World.Instance.AddStateToWorldState(WorldStates.YouAllGonaDie);
        Invoke("WeGood", 10);
    }

    public void WeGood()
    {
        World.Instance.RemoveStateFromWorldState(WorldStates.YouAllGonaDie);
    }
}
