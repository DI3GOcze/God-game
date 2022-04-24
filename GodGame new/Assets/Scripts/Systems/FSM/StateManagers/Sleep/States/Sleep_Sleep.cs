using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
// TODO !!!!
//
public class Sleep_Sleep : StateBase
{
    SleepManager Manager;

    public Sleep_Sleep(SleepManager Manager)
    {
        this.Manager = Manager;
    }

    public override void EnterState()
    {
        base.EnterState();
        // Make agent invisible and deactivate his colliders
        // only if he is sleeping in tent
        // this simulates going inside
        if(Manager.targetTent != null)
            DisableAgent();
    }

    public override void ExitState()
    {
        base.ExitState();
        EnableAgent();
        if(Manager.targetTent != null)
            Manager.targetTent.ReleaseSpot(Manager.Agent.gameObject);
    }

    private void DisableAgent()
    {
        Manager.Agent.GetComponent<Rigidbody>().detectCollisions = false;
        
        foreach (var item in Manager.Agent.GetComponentsInChildren<Renderer>())
        {
            item.enabled = false;
        }

        foreach (var item in Manager.Agent.GetComponents<Collider>())
        {
            item.enabled = false;
        }
    }

    private void EnableAgent()
    {
        Manager.Agent.GetComponent<Rigidbody>().detectCollisions = true;

        foreach (var item in Manager.Agent.GetComponentsInChildren<Renderer>())
        {
            item.enabled = true;
        }

        foreach (var item in Manager.Agent.GetComponents<Collider>())
        {
            item.enabled = true;
        }
    }
}
