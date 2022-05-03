using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAgentBase : MonoBehaviour
{
    protected NavMeshAgent Agent;
    public bool ReachedDestination = false;
    protected virtual void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        CalculateReachedDestination();
    }

    private void CalculateReachedDestination()
    {
        ReachedDestination = false;
        // Can be disabled by VR interaction
        if(Agent.enabled && Agent.isOnNavMesh)
        {
            // If agent isnt calculating path
            if (!Agent.pathPending && !ReachedDestination){
                // If the agent is close enough to the destination
                if (Agent.remainingDistance <= Agent.stoppingDistance){
                    // If the agent has a path or osnt moving
                    // Sometimes if agent is to close to destination it wouldnt create path,
                    // thats why we test his velocity
                    if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f){
                        ReachedDestination = true;
                    }
                }
            }
                       
            if (ReachedDestination)
            {
                Agent.updateRotation = false;
                Agent.isStopped = true;
            }
        }
    }

    /// <summary>
    /// Sets destination point for NavAgent
    /// </summary>
    /// <param name="destinationObject">Desired destination</param>
    /// <returns>True if destination was set successfuly</returns>
    public bool GoToDestination(Vector3 destination)
    {
        bool retVal = Agent.SetDestination(destination);
        CalculateReachedDestination();
        Agent.updateRotation = true;
        Agent.isStopped = false;
        return retVal;
    }

    /// <summary>
    /// Sets destination object for NavAgent
    /// The destination point is the closest point on collider of given gameobject
    /// </summary>
    /// <param name="destinationObject">Desired destination object</param>
    /// <returns>True if destination was set successfuly</returns>
    public bool GoToDestination(GameObject destinationObject)
    {
        Collider collider = destinationObject.GetComponentInChildren<Collider>();
        if (collider == null)
            return GoToDestination(destinationObject.transform.position);

        // Closest point on destination object
        Vector3 closestPoint = collider.ClosestPointOnBounds(gameObject.transform.position);
        return GoToDestination(closestPoint);
    }

    /// <summary>
    /// Kills the Agent
    /// </summary>
    public void Die()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// From given objects finds the closest one
    /// </summary>
    /// <param name="objects">objects for searching</param>
    /// <returns>Closest object</returns>
    public T GetClosestObject<T>(T[] objects) where T : MonoBehaviour
    {
        T closestObject = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = gameObject.transform.position;
        foreach (T o in objects)
        {
            if (o != null)
            {
                float dist = Vector3.Distance(o.transform.position, currentPos);
                if (dist < minDist)
                {
                    closestObject = o;
                    minDist = dist;
                }
            }
        }

        return closestObject;
    }

    /// <summary>
    /// Stops agent from moving
    /// </summary>
    public void StopMovement()
    {
        if (Agent.isOnNavMesh)
        {
           Agent.isStopped = true; 
        }
    }

    protected virtual void OnDestroy() {
        
    }
}
