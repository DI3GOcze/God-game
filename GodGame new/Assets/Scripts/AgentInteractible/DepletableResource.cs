using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class DepletableResource : AgentInteractibleBase
{
    public bool isDepleted = false;
    public float harvestingTime = 5f;
    public int harvestingAmount = 20;

    public delegate void DepletadAction();
    public event DepletadAction OnDepleted;  

    public ResourceTypes resourceType;

    /// <summary>
    /// Object that is activated when resource was depleted
    /// </summary>
    public DepletedResource depletedObject;

    protected override void Start() {
        base.Start();
        currentResourceHealth = resourceHealth;
    }

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

    public float resourceHealth = 10;
    // Health of one resource
    // If health decreses to 0 one resource should be deleted
    public float currentResourceHealth;

    // If object recieves fire damage, its resources should lower
    // returns false if object has been depleted
    public void GiveFireDamage(float damage)
    {
        if (isDepleted)
            return;
        
        currentResourceHealth -= damage;

        if(currentResourceHealth <= 0)
        {           
            int retVal = GetResource(resourceType, 1);

            // If there is no resource left, set resource as depleted
            if (retVal == 0) {
                SetDepleted();
            }

            currentResourceHealth = resourceHealth;
        }   
     
    }

    public void SetDepleted()
    {
        OnDepleted?.Invoke();
        // If depleted resource object isnt set, delete this object forever
        if(depletedObject == null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        World.Instance.RemoveResourceInstance(this);
        
        depletedObject.gameObject.SetActive(true);
        gameObject.SetActive(false);
        isDepleted = true;
    }


    virtual public void ResetResource()
    {
        World.Instance.AddNewResourceInstance(this);
        
        gameObject.SetActive(true);
        depletedObject.gameObject.SetActive(false);
        isDepleted = false;
    }
}
