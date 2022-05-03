using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Implements VR interaction with agent 
/// </summary>
public class AgentVRInteractibleHandler : VRInteractionHandler
{   
    [SerializeField] GPlanner _planner; 
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Rigidbody _rigidBody;
    [SerializeField] float stopFallingSpeed = 0.1f;

    public OnButtonPress leftHandListener;
    public OnButtonPress rightHandListener;

    private IEnumerator coroutine;
    
    public XRRayInteractor leftHandInteractor;
    public XRRayInteractor rightHandInteractor;

    private void Start() {
        leftHandInteractor = GameObject.Find("LeftHand Controller").GetComponent<XRRayInteractor>();
        rightHandInteractor = GameObject.Find("RightHand Controller").GetComponent<XRRayInteractor>();
    }

    /// <summary>
    /// Is called when agent is grabbed with controllers
    /// </summary>
    public override void OnSelectEntered()
    {
        if(coroutine != null) {
            StopCoroutine(coroutine);
        }

        if (_agent != null) { 
            _agent.enabled = false; 
        }
            
        if (_planner != null) { 
            _planner.enabled = false; 
        }

        EnableMenuButtonListener();
    }

    /// <summary>
    /// Enables listening for cotroller buttons
    /// </summary>
    public void EnableMenuButtonListener()
    {
        var leftHandObjects = leftHandInteractor.interactablesSelected;
        var rightHandObjects = rightHandInteractor.interactablesSelected;


        // Iterate throug all objects held by left hand
        if(leftHandObjects.Count > 0)
        {
            foreach (var item in leftHandObjects)
            {
                // If this object is held this hand
                if(item.transform.gameObject == this.gameObject)
                {
                    this.leftHandListener.enabled = true;
                    return;
                }    
            }
        }

        if(rightHandObjects.Count > 0)
        {
            // Iterate throug all objects held by right hand
            foreach (var item in rightHandObjects)
            {
                // If this object is held this hand
                if(item.transform.gameObject == this.gameObject)
                {
                   this.rightHandListener.enabled = true;
                   return;
                }   
            }
        }
    }

    /// <summary>
    /// Disables listening for cotroller buttons
    /// </summary>
    public void DisableMenuButtonListener()
    {
        this.rightHandListener.enabled = false;
        this.leftHandListener.enabled = false;     
    }

    /// <summary>
    /// Is calles when agent is released from hand
    /// </summary>
    public override void OnSelectExited()
    {
        if (_rigidBody != null) 
            { _rigidBody.isKinematic = false; }
            
        coroutine = EnableAfterStacionary();
        StartCoroutine(coroutine);

        DisableMenuButtonListener();
    }

    /// <summary>
    /// Returns agent to his normal state after he has been thrown
    /// </summary>
    IEnumerator EnableAfterStacionary()
    {
        yield return new WaitForSeconds(0.5f);
        if(_rigidBody != null)
        {
            // Until not stacionary
            while (Mathf.Abs(_rigidBody.velocity.x)  > stopFallingSpeed || Mathf.Abs(_rigidBody.velocity.y)  > stopFallingSpeed || Mathf.Abs(_rigidBody.velocity.z) > stopFallingSpeed)
            {
                yield return 0;
            }
        }   


        // Retunr to normal autonomus state
        if (_rigidBody != null) 
            { _rigidBody.isKinematic = true; }

        if (_agent != null) 
            { _agent.enabled = true; }

        if (_planner != null) 
            { _planner.enabled = true; }
    }
}
