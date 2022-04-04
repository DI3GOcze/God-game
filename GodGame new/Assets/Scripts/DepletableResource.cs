using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class DepletableResource : AgentInteractibleBase
{
    public float harvestingTime = 5f;
    public int harvestingAmount = 20;

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
                Destroy(this.gameObject);
            }
        }            
        else
            actualAmmount = 0;

        return actualAmmount;
    }
}
