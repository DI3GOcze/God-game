using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

// Manages FSM states
public abstract class StateManagerBase : MonoBehaviour, IStateManager
{
    public GAgentBase Agent;
    public StateBase currentState;
    protected TextMeshProUGUI StateText;

    /// <summary>
    /// Is called when parent action has been activated
    /// </summary>
    public virtual void OnActivated()
    {
        Agent = GetComponent<GAgentBase>();
        StateText = GetComponentInChildren<Canvas>()?.transform.Find("StateText")?.GetComponent<TextMeshProUGUI>();
        UpdateStateText();
        currentState.EnterState();
    }

    /// <summary>
    /// OnTick is called every frame if parent GOAP action is active
    /// </summary>
    public virtual void OnTick()
    {
        currentState.UpdateState();
    }

    /// <summary>
    /// Is called if parent action is deactivated
    /// </summary>
    public virtual void OnDeactivated()
    {
        currentState.ExitState();
        currentState = null;
    }

    /// <summary>
    /// Deactivates current state, and activates selected one
    /// </summary>
    /// <param name="nextState">Selected state</param>
    public virtual void SwitchState(StateBase nextState)
    {
        currentState.ExitState();
        currentState = nextState;
        UpdateStateText();
        currentState.EnterState();
    }

    // Initializes values for a new run of the same machine
    public virtual void ResetManager() { }

    // Updates debug texts
    protected void UpdateStateText()
    {
        if (StateText != null)
        {
            StateText.text = currentState.GetType().ToString();
        }
    }

}
