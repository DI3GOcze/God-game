using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat_EatFood : StateBase
{
    EatManager Manager;
    int FoodConsumption = 1;
    public Eat_EatFood(EatManager Manager)
    {
        this.Manager = Manager;
    }

    public override void EnterState()
    {
        base.EnterState();
        // If canteen was destroyed or it does not have enough food, find new one
        if(Manager.targetCanteen == null || Manager.targetCanteen.GetResource(ResourceTypes.FOOD, FoodConsumption) != FoodConsumption)
        {
            Manager.SwitchState(Manager.goToCanteenState);
        }
        else
        {
            Manager.StartCoroutine("SimulateEating");
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(Manager.eatingCompleted)
        {
            ((Person)Manager.Agent).ClearHunger();
            Manager.SwitchState(Manager.foodEatenState);
        }   
    }

    public override void ExitState()
    {
        base.ExitState();
        Manager.ResetManager();
    }
}