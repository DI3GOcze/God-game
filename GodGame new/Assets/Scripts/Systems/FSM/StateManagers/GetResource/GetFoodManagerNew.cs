using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetFoodManager : GetResourceManager
{   
    public override System.Type targetResourceSource 
    { 
        get { return typeof(BerriesResource); } 
    }
    public override ResourceTypes targetResourceType 
    { 
        get { return ResourceTypes.FOOD; } 
    }

    public override DepletableResource[] GetValidResources() => World.Instance.GetFreeResource<BerriesResource>().ToArray();
    public override AgentInteractibleBase[] GetDeliverTargets() => World.Instance.GetFreeResource<Canteen>().ToArray();
}
