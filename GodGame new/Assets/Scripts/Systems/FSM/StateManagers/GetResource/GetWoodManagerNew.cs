using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetWoodManager : GetResourceManager
{   
    public override System.Type targetResourceSource 
    { 
        get { return typeof(TreeResource); } 
    }
    public override ResourceTypes targetResourceType 
    { 
        get { return ResourceTypes.WOOD; } 
    }

    protected void Awake() {
        resourceProp = transform.Find("Resources")?.Find("Wood").gameObject;
        toolProp = transform.Find("Resources")?.Find("Axe").gameObject;
    }    

    public override DepletableResource[] GetValidResources() => World.Instance.GetFreeResource<TreeResource>().ToArray();
}
