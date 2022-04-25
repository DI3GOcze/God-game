using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeResource : DepletableResource
{
    public int startingWood = 150;
    
    protected override void Awake()
    {
        base.Awake();
        MaxAgantCapacity = 2;
        Resources.AddNewItem(ResourceTypes.WOOD, startingWood);
    }

    public override void ResetResource()
    {
        Resources.AddNewItem(ResourceTypes.WOOD, startingWood);
        base.ResetResource();
    }
}
