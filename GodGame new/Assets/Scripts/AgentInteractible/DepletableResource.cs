using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class DepletableResource : AgentInteractibleBase
{
    public float harvestingTime = 5f;
    public int harvestingAmount = 20;

    /// <summary>
    /// Object that is activated when resource was depleted
    /// </summary>
    public DepletedResource depletedObject;

    public override int GetResource(ResourceTypes resourceType, int ammount)
    {
        int actualAmmount;
        // If it wasnt deleted already
        if(this != null)
        {
            actualAmmount = Resources.SeizeAndDeleteItemOnEmpty(resourceType, ammount);
            // If there is no resource left, delete object
            if(this.IsEmpty())
            {
                SetDepleted();
            }
        }            
        else
            actualAmmount = 0;

        return actualAmmount;
    }

    public void SetDepleted()
    {
        // If depleted resource object isnt set, delete this object forever
        if(depletedObject == null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        World.Instance.RemoveResourceInstance(this);
        
        depletedObject.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }


    virtual public void ResetResource()
    {
        World.Instance.AddNewResourceInstance(this);
        
        gameObject.SetActive(true);
        depletedObject.gameObject.SetActive(false);
    }
}
