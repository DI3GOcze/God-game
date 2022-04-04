using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHaveFreeTime : GActionBase
{
    List<System.Type> _supportedGoals = new List<System.Type>{typeof(GoalHaveFreeTime)};

    public override List<System.Type> GetSupportedGoals()
    {
        return _supportedGoals;
    }

    public override void OnActivated(GGoalBase linkedGoal)
    {
        base.OnActivated(linkedGoal);
        // TODO udelat jinak... testing purpose only 
        GameObject park = GameObject.Find("Park");

        if (park != null)
        {
            agent.GoToDestination(park.transform.position);
        }
    }

}
