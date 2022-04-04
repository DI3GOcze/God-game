using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalGoToSafety : GGoalBase
{
    public override float CalculatePriority()
    {
        return 1000f;
    }

    public override bool CanRun()
    {
        return World.Instance.IsWorldStateSet(WorldStates.YouAllGonaDie);
    }
}
