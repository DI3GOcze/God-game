using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class VRInteractionHandler : MonoBehaviour
{   
    public virtual void OnHoverEntered()
    {

    }

    public virtual void OnHoverExited()
    {

    }

    /// <summary>
    /// When object grabbed
    /// </summary>
    public virtual void OnSelectEntered()
    {

    }

    /// <summary>
    /// When object released from grab
    /// </summary>
    public virtual void OnSelectExited()
    {

    }
}
