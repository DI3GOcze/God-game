using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetResource_DeliverResource : StateBase
{
    GetResourceManager Manager;
    AgentInteractibleBase DeliverTarget;
    GameObject _player;
    bool _reachedPlayer = false;

    public GetResource_DeliverResource(GetResourceManager Manager)
    {
        this.Manager = Manager;
    }

    public override void EnterState()
    {
        base.EnterState();
        Manager.resourceProp?.SetActive(true);

        // Find closest warehouse
        AgentInteractibleBase[] deliverTargets = Manager.GetDeliverTargets();
        DeliverTarget = Manager.Agent.GetClosestObject<AgentInteractibleBase>(deliverTargets);
        // If found deliver the resources
        if(DeliverTarget != null)
        {
            Manager.Agent.GoToDestination(DeliverTarget.gameObject);
        } 

        _player = GameObject.Find("Player");
    }


    public override void UpdateState()
    {
        base.UpdateState();
        if(DeliverTarget == null)
        {
            _reachedPlayer = !Manager.Agent.GoToDestination(_player.gameObject.transform.position);           
        } 
        
        // If reached the warehouse or player, store resources and start  GoToResourceState
        if(Manager.Agent.ReachedDestination || _reachedPlayer)
        {
            Warehouse.warehouseInvetory.AddAmmountOrAddNewItem(Manager.targetResourceType, Manager.ResourceHarvestedAmount);
            Manager.SwitchState(Manager.GoToAResourceState);
        }
    }

    
    public override void ExitState()
    {
        base.ExitState();
        Manager.resourceProp?.SetActive(false);
        DeliverTarget = null;
        Manager.ResetManager();
    }
}