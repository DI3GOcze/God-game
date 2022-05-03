
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[System.Serializable]
public struct OnActionPair {
    public InputAction action;

    // When the button is pressed
    public UnityEvent OnPress;

    // When the button is released
    public UnityEvent OnRelease;

    public void Initialize()
    {
        action.started += Pressed;
        action.canceled += Released;
    }

    public void Destroy()
    {
        action.started -= Pressed;
        action.canceled -= Released;
    }

    private void Pressed(InputAction.CallbackContext context)
    {
        OnPress.Invoke();
    }

    private void Released(InputAction.CallbackContext context)
    {
        OnRelease.Invoke();
    }

    public void Enable()
    {
        action.Enable();
    }

    public void Disable()
    {
        action.Disable();
    }
}

/// <summary>
/// Implmentation of listener to actions. Listeners and action 
/// can be set in Inspector
/// </summary>
public class OnButtonPress : MonoBehaviour
{
    public List<OnActionPair> OnActionList = new List<OnActionPair>();

    private void Start() {
        foreach (var ActionPair in OnActionList)
        {
            ActionPair.Initialize();
        } 
    }

    private void OnDestroy() {
        foreach (var ActionPair in OnActionList)
        {
            ActionPair.Destroy();
        }
    }

    private void OnEnable()
    {
        foreach (var ActionPair in OnActionList)
        {
            ActionPair.Enable();
        }
    }

    private void OnDisable()
    {
        foreach (var ActionPair in OnActionList)
        {
            ActionPair.Disable();
        }
    }
}
