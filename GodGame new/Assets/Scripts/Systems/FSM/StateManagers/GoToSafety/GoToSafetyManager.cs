using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToSafetyManager : StateManagerBase
{   
    public GoToSafety_GetCover GetCoverState;
    public GoToSafety_StayInCover StayInCover;
    public override void OnActivated()
    {
        currentState = GetCoverState = new GoToSafety_GetCover(this);
        StayInCover = new GoToSafety_StayInCover(this);

        base.OnActivated();
    }
}
