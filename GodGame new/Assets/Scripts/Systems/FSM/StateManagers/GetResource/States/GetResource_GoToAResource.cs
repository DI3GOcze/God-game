using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
// TODO !!!!
//
public class GetResource_GoToAResource : StateBase
{
    GetResourceManager Manager;

    public GetResource_GoToAResource(GetResourceManager Manager)
    {
        this.Manager = Manager;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        // If no resource was found (or found resource is depleted or destroyed)
        if(Manager.TargetResource == null || Manager.TargetResource?.gameObject.activeInHierarchy == false)
        {
            // Find closest resource
            DepletableResource[] freeResources = Manager.GetValidResources();     
            Manager.TargetResource = Manager.Agent.GetClosestObject<DepletableResource>(freeResources);
            // If found, seize that resource and to that resource
            if(Manager.TargetResource != null)
            {
                Manager.TargetResource.SeizeSpot(Manager.Agent.gameObject);
                Manager.Agent.GoToDestination(Manager.TargetResource.gameObject);
            }
        } 
        // If reached resource, start harvesting state
        else if(Manager.Agent.ReachedDestination)
        {
            Manager.SwitchState(Manager.HarvestResourceState);
        }
            
    }
}
