using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSafety_GetCover : StateBase
{
    GoToSafetyManager Manager;
    Vector3 targetCover;

    public GoToSafety_GetCover(GoToSafetyManager Manager)
    {
        this.Manager = Manager;
    }

    public override void EnterState()
    {
        base.EnterState();
        targetCover = FindCover();
        Manager.Agent.GoToDestination(targetCover);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(Manager.Agent.ReachedDestination)
            Manager.SwitchState(Manager.StayInCover);
    }

    private Vector3 FindCover()
    {
        return GameObject.Find("Safe").transform.position;
    }
}
