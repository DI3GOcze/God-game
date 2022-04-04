using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using TMPro;
#endif  

public class GPlanner : MonoBehaviour
{
    private List<GGoalBase> Goals;
    private List<GActionBase> Actions;
    private GGoalBase ActiveGoal;
    private GActionBase ActiveAction;

    void Start()
    {
        UpdateGoalsAndActions();
    }

    // Update is called once per frame
    void Update()
    {
        // Find goal with highest priority and action with lowest cost
        GGoalBase bestGoal = null;
        GActionBase bestAction = null;
        foreach (GGoalBase goal in Goals)
        {
            if (!goal.CanRun())
                continue;

            // If this goal has lower priority than bestGoal
            if (bestGoal != null && goal.CalculatePriority() < bestGoal.CalculatePriority())
                continue;

            GActionBase bestActionForThisGoal = null;
            foreach (GActionBase action in Actions)
            {
                // If the action doesnt lead to this goal
                if (!action.GetSupportedGoals().Contains(goal.GetType()))
                    continue;

                // If the action is cheaper then currently the chepest action for this goal
                if (bestActionForThisGoal == null || action.GetCost() < bestActionForThisGoal.GetCost())
                    bestActionForThisGoal = action;
            }

            // If the goal is rechable its the best goal
            if (bestActionForThisGoal != null)
            {
                bestGoal = goal;
                bestAction = bestActionForThisGoal;
            }
        }

        // No active goal
        if (ActiveGoal == null)
        {
            ActiveGoal = bestGoal;
            ActiveAction = bestAction;

            if (ActiveGoal != null)
            {
                ActiveGoal.OnActivated(ActiveAction);
                ActiveAction.OnActivated(ActiveGoal);
            }
        }
        // Goal didnt change
        else if (ActiveGoal == bestGoal)
        {
            // If action changed
            if (ActiveAction != bestAction)
            {
                // Change Action
                ActiveAction.OnDeactivated();
                ActiveAction = bestAction;
                ActiveAction.OnActivated(ActiveGoal);
            }
        }
        // Goal changed or no valid goal was found
        else
        {
            // Deactivate current goal
            ActiveGoal.OnDeactivated();
            ActiveAction.OnDeactivated();

            ActiveGoal = bestGoal;
            ActiveAction = bestAction;

            if (ActiveGoal != null)
            {
                ActiveGoal.OnActivated(ActiveAction);
                ActiveAction.OnActivated(ActiveGoal);
            }
        }

        // Run active action
        if (ActiveAction != null)
            ActiveAction.OnTick();

    }
    public void UpdateGoalsAndActions()
    {
        Goals = new List<GGoalBase>(GetComponents<GGoalBase>());
        Actions = new List<GActionBase>(GetComponents<GActionBase>());
    }

    private void OnDestroy()
    {
        ActiveAction?.OnDeactivated();
        ActiveGoal?.OnDeactivated();
    }

    private void OnDisable() {
        ActiveGoal!.OnDeactivated();
        ActiveAction!.OnDeactivated();

        ActiveGoal = null;
        ActiveAction = null;
    }

}
