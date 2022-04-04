using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
// TODO !!!!
//
public class GetResource_HarvestResource : StateBase
{
    GetResourceManager Manager;
    private IEnumerator coroutine;

    public GetResource_HarvestResource(GetResourceManager Manager)
    {
        this.Manager = Manager;
    }

    public override void EnterState()
    {
        base.EnterState();   
        // Start harvesting
        coroutine = Manager.SimulateHarvesting(); 
        Manager.StartCoroutine(coroutine);   
    }

    public override void UpdateState()
    {
        base.UpdateState();
        // If harvesting was completed
        if(Manager.HarvestingCompleted)
        {
            // Get resources from resource
            Manager.ResourceHarvestedAmount = Manager.TargetResource.GetResource(Manager.targetResourceType, Manager.TargetResource.harvestingAmount);
            if(Manager.ResourceHarvestedAmount > 0)
            {
                // Some resources were harvested => start delivering state
                Manager.SwitchState(Manager.DeliverResourceState);
            }  
            else 
            {
                // No resources were harvested => Find new closest resource
                Manager.SwitchState(Manager.GoToAResourceState);   
            }
                 
        } 
        // If resource was depleted while hartvesting
        else if(Manager.TargetResource == null || Manager.TargetResource?.gameObject.activeInHierarchy == false)
        {
            // Currently harvested resource was destroyed => stop harvesting and find new resource
            Manager.StopCoroutine(coroutine);
            Manager.SwitchState(Manager.GoToAResourceState); 
        }  
    }

    public override void ExitState()
    {
        base.ExitState();
        Manager.TargetResource.ReleaseSpot(Manager.Agent.gameObject);
        Manager.TargetResource = null;
    }
}
