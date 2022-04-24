using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep_GoToTent : StateBase
{
    SleepManager Manager;
    GameObject targetSleepSpace;

    // Server for exiting state pupouses
    private bool _goingToSleep = false;

    public Sleep_GoToTent(SleepManager Manager)
    {
        this.Manager = Manager;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        // If no tent was found (or found tent was destroyed)
        if(targetSleepSpace == null)
        {
            // Find closest free tent
            AgentInteractibleBase[] freeTents = Manager.GetTents();    
            if(freeTents.Length == 0) {
                targetSleepSpace = Manager.Agent.GetClosestObject<CampFire>(World.Instance.campFires.ToArray()).gameObject;
            } else {
                Manager.targetTent = Manager.Agent.GetClosestObject<AgentInteractibleBase>(freeTents);
                targetSleepSpace = Manager.targetTent.gameObject;
            }
            
            // If found tent, seize spot
            if(Manager.targetTent != null) {
                Manager.targetTent.SeizeSpot(Manager.Agent.gameObject);
            }

            Manager.Agent.GoToDestination(targetSleepSpace);
            _goingToSleep = true;
        } 
        // If reached destination, sleep
        if(Manager.Agent.ReachedDestination)
        {
            _goingToSleep = false;
            Manager.SwitchState(Manager.sleepState);
        }    
    }

    public override void ExitState()
    {
        base.ExitState();
        // If villager was on his way to tent and got interupted -> release spot
        if(_goingToSleep) {
            if(Manager.targetTent != null){
                Manager.targetTent.ReleaseSpot(Manager.Agent.gameObject);
            }
        }
    }
}