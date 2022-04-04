using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GetResourceManager : StateManagerBase
{   
    public GetResource_GoToAResource GoToAResourceState;
    public GetResource_HarvestResource HarvestResourceState;
    public GetResource_DeliverResource DeliverResourceState;
    public int ResourceHarvestedAmount = 0;
    public DepletableResource TargetResource;
    public bool HarvestingCompleted = false;
    public abstract System.Type targetResourceSource { get; }
    public abstract ResourceTypes targetResourceType { get; }
    public abstract DepletableResource[] GetValidResources();
    public virtual AgentInteractibleBase[] GetDeliverTargets() => World.Instance.GetFreeResource<Warehouse>().ToArray();
    public override void OnActivated()
    {
        ResetManager();
        currentState = GoToAResourceState = new GetResource_GoToAResource(this);
        HarvestResourceState = new GetResource_HarvestResource(this);
        DeliverResourceState = new GetResource_DeliverResource(this);
        base.OnActivated();
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        ResetManager();
    }

    public override void ResetManager()
    {
        ResourceHarvestedAmount = 0;
        // Release seized resource
        if(TargetResource != null)
        {
            TargetResource.ReleaseSpot(Agent.gameObject);
            TargetResource = null;
        }
        HarvestingCompleted = false;
    }

    public IEnumerator SimulateHarvesting()
    {
        float duration = ((Grown)Agent).strenghEfficiency * TargetResource.harvestingTime;
        yield return new WaitForSeconds(duration);
        HarvestingCompleted = true;
    }

}
