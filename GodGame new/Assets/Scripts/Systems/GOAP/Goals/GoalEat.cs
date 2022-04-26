using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEat : GGoalBase
{
    public override float CalculatePriority()
    {
        if (Agent != null && Agent.GetType().IsSubclassOf(typeof(Person)))
            return ((Person)Agent).HungerNormalized * 90;

        return 0;
    }

    public override bool CanRun()
    {
        if (Agent != null && Agent.GetType().IsSubclassOf(typeof(Person)) && ((Person)Agent).HungerNormalized > 0.5f)
            return true;  
        
        return false;
    }
}
