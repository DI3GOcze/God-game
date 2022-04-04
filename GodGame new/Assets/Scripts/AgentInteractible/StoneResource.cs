using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneResource : DepletableResource
{

    public int startingStone = 60;

    protected override void Awake()
    {
        base.Awake();
        MaxAgantCapacity = 3;
        Resources.AddNewItem(ResourceTypes.STONE, startingStone);
    }

    public override void ResetResource()
    {
        Resources.AddNewItem(ResourceTypes.STONE, startingStone);
        base.ResetResource();
    }
}
