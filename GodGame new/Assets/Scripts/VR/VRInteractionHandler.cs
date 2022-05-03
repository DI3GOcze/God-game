using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Helping for objects listening for VR acitons
/// </summary>
public abstract class VRInteractionHandler : MonoBehaviour
{   
    /// <summary>
    /// Called when interaction hovered above object
    /// </summary>
    public virtual void OnHoverEntered()
    {

    }

    /// <summary>
    /// Called when interaction stopped hovering above object
    /// </summary>
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
