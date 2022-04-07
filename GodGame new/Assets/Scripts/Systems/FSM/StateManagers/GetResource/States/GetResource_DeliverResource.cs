using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetResource_DeliverResource : StateBase
{
    GetResourceManager Manager;
    AgentInteractibleBase DeliverTarget;

    public GetResource_DeliverResource(GetResourceManager Manager)
    {
        this.Manager = Manager;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(DeliverTarget == null)
        {
            // Find closest warehouse
            AgentInteractibleBase[] deliverTargets = Manager.GetDeliverTargets();
            DeliverTarget = Manager.Agent.GetClosestObject<AgentInteractibleBase>(deliverTargets);
            // If found deliver the resources
            if(DeliverTarget != null)
            {
                Manager.Agent.GoToDestination(DeliverTarget.gameObject);
            }            
        } 
        // If reached the warehouse, store resources and start  GoToResourceState
        else if(Manager.Agent.ReachedDestination)
        {
            DeliverTarget.StoreResource(Manager.targetResourceType, Manager.ResourceHarvestedAmount);
            Manager.SwitchState(Manager.GoToAResourceState);
        }
    }

    public override void EnterState()
    {
        base.EnterState();
        Manager.resourceProp?.SetActive(true);
    }

    public override void ExitState()
    {
        base.ExitState();
        Manager.resourceProp?.SetActive(false);
        DeliverTarget = null;
        Manager.ResetManager();
    }
}