using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalGetStone : GGoalBase
{
    public override float CalculatePriority()
    {
        return 70f;
    }

    public override bool CanRun()
    {
        //if(World.Instance.timeOfDay == TimesOfDay.Afternoon && (IsActive || World.Instance.CountOfFreeResource<StoneResource>() > 0)) Too expensive
        // If is already active or there is resource to be harvested
        if (World.Instance.timeOfDay == TimesOfDay.Afternoon)
            return true;
        else
            return false;
    }
}
