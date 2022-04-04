using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerriesResource : DepletableResource
{
    public int startingFood = 300;

    protected override void Awake()
    {
        base.Awake();
        MaxAgantCapacity = 1;
        Resources.AddNewItem(ResourceTypes.FOOD, startingFood);
    }

    public override void ResetResource()
    {
        Resources.AddNewItem(ResourceTypes.FOOD, startingFood);
        base.ResetResource();
    }
}
