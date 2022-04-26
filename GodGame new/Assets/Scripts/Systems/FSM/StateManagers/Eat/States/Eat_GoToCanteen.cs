using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
// TODO !!!!
//
public class Eat_GoToCanteen : StateBase
{
    EatManager Manager;

    public Eat_GoToCanteen(EatManager Manager)
    {
        this.Manager = Manager;
    }

    public override void EnterState()
    {
        base.EnterState();
        
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        if(Manager.targetFoodSource == null)
        {
            // Find closest canteen
            AgentInteractibleBase[] freeCanteens = Manager.GetCanteens();     
            Manager.targetFoodSource = Manager.Agent.GetClosestObject<AgentInteractibleBase>(freeCanteens);
            
            // If no canteen was found, find closest food source
            if(Manager.targetFoodSource == null)
            {
                var foodSources = World.Instance.GetFreeResource<BerriesResource>().ToArray();
                Manager.targetFoodSource = Manager.Agent.GetClosestObject<AgentInteractibleBase>(foodSources);
            }

            Manager.Agent.GoToDestination(Manager.targetFoodSource.gameObject);
        }
        
        if(Manager.Agent.ReachedDestination)
        {
            Manager.SwitchState(Manager.eatFoodState);
        }     
    }
}
