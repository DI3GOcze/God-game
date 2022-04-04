using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoal
{
    float CalculatePriority();
    bool CanRun();

    void OnTickGoal();
    void OnActivated(GActionBase _linkedAction);
    void OnDeactivated();
}

public abstract class GGoalBase : MonoBehaviour, IGoal
{
    protected GAgentBase Agent;
    protected GActionBase LinkedAction;
    protected bool IsActive = false;

    void Awake()
    {
        Agent = GetComponent<GAgentBase>();
    }

    void Update()
    {
        OnTickGoal();
    }

    public virtual float CalculatePriority()
    {
        // Priorities based on priority:
        // Low priority 0 - 30
        // Medium priority 30 - 70
        // High priority 70 - 100
        // Must Happen 1000   

        // Proffesion jobs -> 70

        return -1f;
    }

    public virtual bool CanRun()
    {
        return false;
    }

    public virtual void OnTickGoal()
    {
        // Update internal state (for CalculatePriorityPriority, CanRun etc.)
    }

    public virtual void OnActivated(GActionBase _linkedAction)
    {
        IsActive = true;
        LinkedAction = _linkedAction;
    }

    public virtual void OnDeactivated()
    {
        IsActive = false;
        LinkedAction = null;
    }
}
