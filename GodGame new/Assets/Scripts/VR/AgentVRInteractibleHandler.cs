using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentVRInteractibleHandler : VRInteractionHandler
{   
    [SerializeField] GPlanner _planner; 
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Rigidbody _rigidBody;
    [SerializeField] float stopFallingSpeed = 0.1f;
    private IEnumerator coroutine;
        

    public override void OnSelectEntered()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        if (_agent != null) 
            { _agent.enabled = false; }
            
        if (_planner != null) 
            { _planner.enabled = false; }
    }

    public override void OnSelectExited()
    {
        if (_rigidBody != null) 
            { _rigidBody.isKinematic = false; }
            
        coroutine = EnableAfterStacionary();
        StartCoroutine(coroutine);
    }

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


        if (_rigidBody != null) 
            { _rigidBody.isKinematic = true; }

        if (_agent != null) 
            { _agent.enabled = true; }

        if (_planner != null) 
            { _planner.enabled = true; }

 
    }
}
