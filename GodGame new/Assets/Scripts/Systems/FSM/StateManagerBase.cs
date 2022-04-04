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

    // Current state must be set before base.OnActivated()
    public virtual void OnActivated()
    {
        Agent = GetComponent<GAgentBase>();
        StateText = GetComponentInChildren<Canvas>()?.transform.Find("StateText")?.GetComponent<TextMeshProUGUI>();
        UpdateStateText();
        currentState.EnterState();
    }

    public virtual void OnTick()
    {
        currentState.UpdateState();
    }

    public virtual void OnDeactivated()
    {
        currentState.ExitState();
        currentState = null;
    }

    public virtual void SwitchState(StateBase nextState)
    {
        currentState.ExitState();
        currentState = nextState;
        UpdateStateText();
        currentState.EnterState();
    }

    // Initializes values for a new run of the same machine
    public virtual void ResetManager() { }

    protected void UpdateStateText()
    {
        if (StateText != null)
        {
            StateText.text = currentState.GetType().ToString();
        }
    }

}
