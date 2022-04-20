using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSleep : GGoalBase
{
    public override float CalculatePriority()
    {
        return 70;
    }

    public override bool CanRun()
    {
        if (World.Instance.timeOfDay == TimesOfDay.Night)
            return true;
        else
            return false;
    }
}
