using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public abstract class AgentInteractibleBase : MonoBehaviour
{
    public Canvas ResourcePanel;
    public TextMeshProUGUI WoodText;
    public TextMeshProUGUI StoneText;
    public TextMeshProUGUI FoodText;
    public int FreeSpots => MaxAgantCapacity - AssignedAgents.Count; 
    
    [SerializeField] public ResourceTypesInventory Resources;       
    // This cannot be changed dynamicly
    public int MaxAgantCapacity  { get; protected set; } = 1;
    [SerializeField] protected List<GameObject> AssignedAgents;
    protected virtual void Awake()
    {
        AssignedAgents = new List<GameObject>();
        Resources = new ResourceTypesInventory();
    }
 
    protected virtual void Start() {
        World.Instance.AddNewResourceInstance(this);
    }

    protected virtual void OnDestroy() {
        World.Instance.RemoveResourceInstance(this);
    }

    protected virtual void InitializeResource()
    {
        
    }

    public virtual bool SeizeSpot(GameObject agent)
    {
        if(FreeSpots < 1)
            return false;

        AssignedAgents.Add(agent);
        return true;
    }

    public virtual void ReleaseSpot(GameObject agent)
    {
        AssignedAgents.Remove(agent);
    } 

    public virtual void StoreResource(ResourceTypes resourceType, int ammount)
    {
        Resources.AddAmmountOrAddNewItem(resourceType, ammount);
    }

    public virtual int GetResource(ResourceTypes resourceType, int ammount)
    {
        int actualAmmount = Resources.SeizeAndDeleteItemOnEmpty(resourceType, ammount);
        return actualAmmount;
    }

    public bool IsEmpty()
    {
        if(Resources != null)
            return Resources.IsEmpty();
        
        return true;
    }

    protected virtual void UpdateGUI()
    {
        if(ResourcePanel != null && Resources != null && Resources.Items != null)
        {
            string woodNum;
            if(Resources.Items.ContainsKey(ResourceTypes.WOOD))
                woodNum = Resources.Items[ResourceTypes.WOOD].ToString();
            else
                woodNum = "0";

            string stoneNum;
            if(Resources.Items.ContainsKey(ResourceTypes.STONE))
                stoneNum = Resources.Items[ResourceTypes.STONE].ToString();
            else
                stoneNum = "0";

            string foodNum = "0";
            if(Resources.Items.ContainsKey(ResourceTypes.FOOD))
                foodNum = Resources.Items[ResourceTypes.FOOD].ToString();
            else
                foodNum = "0";

            if(WoodText != null)
                WoodText.text = "Wood " + woodNum;

            if(StoneText != null)
                StoneText.text = "Stone " + stoneNum;

            if(FoodText != null)
                FoodText.text = "Food " + foodNum;
        }            
    }
}
