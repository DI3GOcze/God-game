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

    /// <summary>
    /// Returns current priority of goal
    /// </summary>
    /// <returns>Priority of goal</returns>
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

    /// <summary>
    /// Determines if goal can be activated
    /// </summary>
    /// <returns>True if can be activated</returns>
    public virtual bool CanRun()
    {
        return false;
    }

    /// <summary>
    /// Is called every frame if goal is active
    /// </summary>
    public virtual void OnTickGoal()
    {
        // Update internal state (for CalculatePriorityPriority, CanRun etc.)
    }

    /// <summary>
    /// Is called once goal has been activated
    /// </summary>
    /// <param name="_linkedAction">Action that has been activated</param>
    public virtual void OnActivated(GActionBase _linkedAction)
    {
        IsActive = true;
        LinkedAction = _linkedAction;
    }

    /// <summary>
    /// Is called once goal has been deactivated
    /// </summary>
    public virtual void OnDeactivated()
    {
        IsActive = false;
        LinkedAction = null;
    }
}
