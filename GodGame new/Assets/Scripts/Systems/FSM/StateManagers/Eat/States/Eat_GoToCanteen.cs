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

    public override void UpdateState()
    {
        
        base.UpdateState();
        // If no resource was found (or found resource was destroyed)
        if(Manager.targetCanteen == null)
        {
            // Find closest resource
            AgentInteractibleBase[] freeCanteens = Manager.GetCanteens();     
            Manager.targetCanteen = Manager.Agent.GetClosestObject<AgentInteractibleBase>(freeCanteens);
            // If found, seize that resource and go to that resource
            if(Manager.targetCanteen != null)
            {
                Manager.Agent.GoToDestination(Manager.targetCanteen.gameObject);
            }
        } 
        // If reached resource, start harvesting state
        else if(Manager.Agent.ReachedDestination)
        {
            Manager.SwitchState(Manager.eatFoodState);
        }
            
    }
}
