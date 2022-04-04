using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHaveFreeTime : GGoalBase
{
    public override float CalculatePriority()
    {
        return 10f;
    }

    public override bool CanRun()
    {
        return true;
    }
}
