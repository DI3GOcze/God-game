using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep_GoToTent : StateBase
{
    SleepManager Manager;
    int FoodConsumption = 1;
    public Sleep_GoToTent(SleepManager Manager)
    {
        this.Manager = Manager;
    }

    public override void UpdateState()
    {
        
        base.UpdateState();
        // If no tent was found (or found tent was destroyed)
        if(Manager.targetTent == null)
        {
            // Find closest free tent
            AgentInteractibleBase[] freeTents = Manager.GetTents();     
            Manager.targetTent = Manager.Agent.GetClosestObject<AgentInteractibleBase>(freeTents);
            // If found, seize that tent and go to that tent
            if(Manager.targetTent != null)
            {
                Manager.targetTent.SeizeSpot(Manager.Agent.gameObject);
                Manager.Agent.GoToDestination(Manager.targetTent.gameObject);
            }
        } 
        // If reached resource, start harvesting state
        else if(Manager.Agent.ReachedDestination)
        {
            Manager.SwitchState(Manager.sleepState);
        }
            
    }
}