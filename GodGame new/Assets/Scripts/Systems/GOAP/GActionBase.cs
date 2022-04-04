using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GActionBase : MonoBehaviour
{
    protected GAgentBase agent;
    protected GGoalBase linkedGoal;

    void Awake()
    {
        agent = GetComponent<GAgentBase>();
    }

    // Returns goals that can be reached with this action
    public virtual List<System.Type> GetSupportedGoals()
    {
        return null;
    }

    // Shoud return cost of this action
    public virtual float GetCost()
    {
        return 0f;
    }

    // This method is run before its first OnTick()
    public virtual void OnActivated(GGoalBase linkedGoal)
    {
        this.linkedGoal = linkedGoal;
    }

    // This method is run on deactivation by planner (last method)
    public virtual void OnDeactivated()
    {
        linkedGoal = null;
        agent.StopMovement();
    }

    // If action is active, this method is run every Update()
    public virtual void OnTick()
    {

    }
}
