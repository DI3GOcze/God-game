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
    public Button ArmagedonButton;
    private Color ButtonBaseColor;
    public Color ButtonActiveColor = Color.red;
    void Start()
    {
        LimitFPS();
        ButtonBaseColor = ArmagedonButton.GetComponent<Image>().color;
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
        ArmagedonButton.GetComponent<Image>().color = ButtonActiveColor;
        ArmagedonButton.enabled = false;

        World.Instance.AddStateToWorldState(WorldStates.YouAllGonaDie);
        Invoke("WeGood", 10);
    }

    public void WeGood()
    {
        ArmagedonButton.GetComponent<Image>().color = ButtonBaseColor;
        ArmagedonButton.enabled = true;

        World.Instance.RemoveStateFromWorldState(WorldStates.YouAllGonaDie);
    }
}
