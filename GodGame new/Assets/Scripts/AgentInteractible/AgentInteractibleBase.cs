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
    // MaxAgentCapacity cannot be changed dynamicly
    public int MaxAgantCapacity  { get; protected set; } = 1;
    [SerializeField] protected List<GameObject> AssignedAgents;
    

    /// <summary>
    /// Is used to reserve spot at resource by agent
    /// </summary>
    /// <param name="agent">Reserving agent</param>
    /// <returns>If reservation was succesful true, else false</returns>
    public virtual bool SeizeSpot(GameObject agent)
    {
        if(FreeSpots < 1)
            return false;

        AssignedAgents.Add(agent);
        return true;
    }

    /// <summary>
    /// Is used to cancel reservation by agent
    /// </summary>
    /// <param name="agent">Reserved agent</param>
    public virtual void ReleaseSpot(GameObject agent)
    {
        AssignedAgents.Remove(agent);
    } 

    /// <summary>
    /// Inserts ammount of resource into resource inventory
    /// </summary>
    /// <param name="resourceType">Type of inserted resource</param>
    /// <param name="ammount">Amount of inserted resource</param>
    public virtual void StoreResource(ResourceTypes resourceType, int ammount)
    {
        Resources.AddAmmountOrAddNewItem(resourceType, ammount);
    }

    /// <summary>
    /// Retrieves amount of resource from resource inventory
    /// </summary>
    /// <param name="resourceType">Type of retrieved resource</param>
    /// <param name="ammount">Amount of retrieved resource</param>
    /// <returns>Returns actual retrieved amount of resource</returns>
    public virtual int GetResource(ResourceTypes resourceType, int ammount)
    {
        int actualAmmount = Resources.SeizeAndDeleteItemOnEmpty(resourceType, ammount);
        return actualAmmount;
    }

    /// <summary>
    /// Is used to determine if resource inventory is empty
    /// </summary>
    /// <returns>True if empty</returns>
    public bool IsEmpty()
    {
        if(Resources != null)
            return Resources.IsEmpty();
        
        return true;
    }

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
}
