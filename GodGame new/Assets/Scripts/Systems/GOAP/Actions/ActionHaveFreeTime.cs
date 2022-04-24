using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActionHaveFreeTime : GActionBase
{
    List<System.Type> _supportedGoals = new List<System.Type>{typeof(GoalHaveFreeTime)};

    public override List<System.Type> GetSupportedGoals()
    {
        return _supportedGoals;
    }

    public float wanderFromOriginRadius = 10f;
    public float maxOneTripTime = 10f;
    public Transform target;
    public Vector3 wanderingOrigin;
    public float timer;
    private bool _isOnTrip = false;
    private Coroutine maxWanderingTimer;

    private void Start() {
        wanderingOrigin = GameObject.Find("WanderingOrigin").transform.position;
    }

    public override void OnActivated(GGoalBase linkedGoal)
    {
        base.OnActivated(linkedGoal);
        timer = maxOneTripTime;
    }

    public override void OnTick()
    {
        base.OnTick();

        if (!_isOnTrip || agent.ReachedDestination) {
            if(maxWanderingTimer != null){
                StopCoroutine(maxWanderingTimer);
            }
            maxWanderingTimer = StartCoroutine(MaxWanderingTimer());
            
            Vector3 newPos = RandomPointInWaderingZone();
            agent.GoToDestination(newPos);
            
            _isOnTrip = true;
        }
    } 

    public Vector3 RandomPointInWaderingZone() {
        Vector3 randomDirection = Random.insideUnitSphere * wanderFromOriginRadius;
 
        randomDirection += wanderingOrigin;
  
        NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, 10f, -1);
        
        Debug.Log(navHit.position); 

        return navHit.position;
    }

    IEnumerator MaxWanderingTimer()
    {
        yield return new WaitForSeconds(maxOneTripTime);
        _isOnTrip = false;
    }

}
