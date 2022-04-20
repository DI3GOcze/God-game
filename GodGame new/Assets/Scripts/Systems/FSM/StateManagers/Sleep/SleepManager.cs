using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SleepManager : StateManagerBase
{   
    public Sleep_GoToTent goToTentState;
    public Sleep_Sleep sleepState;
    public AgentInteractibleBase targetTent;
    public AgentInteractibleBase[] GetTents() => World.Instance.GetFreeResource<Tent>().ToArray();

    public override void OnActivated()
    {
        ResetManager();
        currentState = goToTentState = new Sleep_GoToTent(this);
        sleepState = new Sleep_Sleep(this);
        base.OnActivated();
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        ResetManager();
    }

    public override void ResetManager()
    {
        // Release seized tent
        if(targetTent != null)
        {
            targetTent.ReleaseSpot(Agent.gameObject);
            targetTent = null;
        }
    }
}
