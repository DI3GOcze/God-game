using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public int Trees;
    public int Stones;
    public int Warehouses;
    /// <summary>
    /// Sets current game simulation speed
    /// </summary>
    public float TimeScale = 1f;

    void Start()
    {
        LimitFPS();
    }

    /// <summary>
    /// Sets target frames for quest 2 compatible (90fps)
    /// </summary>
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

    /// <summary>
    /// Sets world state YouAllGonaDie to true
    /// </summary>
    public void Armagedon()
    {
        World.Instance.AddStateToWorldState(WorldStates.YouAllGonaDie);
        Invoke("WeGood", 10);
    }

    /// <summary>
    /// Sets world state YouAllGonaDie to false
    /// </summary>
    public void WeGood()
    {
        World.Instance.RemoveStateFromWorldState(WorldStates.YouAllGonaDie);
    }
}
