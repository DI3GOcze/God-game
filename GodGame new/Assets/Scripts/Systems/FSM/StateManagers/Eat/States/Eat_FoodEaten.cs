using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat_FoodEaten : StateBase
{
    EatManager Manager;

    public Eat_FoodEaten(EatManager Manager)
    {
        this.Manager = Manager;
    }

    // Infinite state, just waiting for goal change

    public override void ExitState()
    {
        base.ExitState();
        Manager.ResetManager();
    }
}