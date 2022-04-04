using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGetWood : GActionBase
{
    List<System.Type> SupportedGoals = new List<System.Type>{typeof(GoalGetWood)};
    GetWoodManager Manager;

    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    // This method is run before its first OnTick()
    public override void OnActivated(GGoalBase _linkedGoal)
    {
        base.OnActivated(_linkedGoal);
        Manager = gameObject.AddComponent<GetWoodManager>();
        Manager.OnActivated();
    }

    public override void OnTick()
    {
        base.OnTick();
        Manager.OnTick();
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        Manager.OnDeactivated();
        Destroy(Manager);
    }

}
