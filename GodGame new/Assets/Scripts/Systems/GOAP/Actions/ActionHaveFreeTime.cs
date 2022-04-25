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
    private Vector3 _wanderingOrigin;
    private bool _isOnTrip = false;
    private Coroutine _maxWanderingTimer;

    public override void OnActivated(GGoalBase linkedGoal)
    {
        base.OnActivated(linkedGoal);
        _wanderingOrigin = transform.position;
        _isOnTrip = false;
    }

    public override void OnTick()
    {
        base.OnTick();

        if (!_isOnTrip || agent.ReachedDestination) {
            if(_maxWanderingTimer != null){
                StopCoroutine(_maxWanderingTimer);
            }
            _maxWanderingTimer = StartCoroutine(MaxWanderingTimer());
            
            Vector3 newPos = RandomPointInWaderingZone();
            agent.GoToDestination(newPos);
            
            _isOnTrip = true;
        }
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        if(_maxWanderingTimer != null){
            StopCoroutine(_maxWanderingTimer);
        }
    }

    public Vector3 RandomPointInWaderingZone() {
        Vector2 randomPointInRadius = Random.insideUnitCircle * wanderFromOriginRadius;
        Vector3 pointInWanderingZone = new Vector3(randomPointInRadius.x, 0, randomPointInRadius.y) + _wanderingOrigin;
  
        if(NavMesh.SamplePosition(pointInWanderingZone, out NavMeshHit navHit, 10f, -1)){
            return navHit.position;
        }
        
        return Vector3.zero; 
    }

    IEnumerator MaxWanderingTimer()
    {
        yield return new WaitForSeconds(maxOneTripTime);
        _isOnTrip = false;
    }
}
