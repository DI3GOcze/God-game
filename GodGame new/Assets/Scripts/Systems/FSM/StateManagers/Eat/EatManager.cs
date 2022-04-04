using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EatManager : StateManagerBase
{   
    public Eat_GoToCanteen goToCanteenState;
    public Eat_EatFood eatFoodState;
    public Eat_FoodEaten foodEatenState;
    public AgentInteractibleBase targetCanteen;
    public AgentInteractibleBase[] GetCanteens() => World.Instance.GetFreeResource<Canteen>().FindAll(x => !x.IsEmpty()).ToArray();
    public bool eatingCompleted = false;


    public override void OnActivated()
    {
        ResetManager();
        currentState = goToCanteenState = new Eat_GoToCanteen(this);
        eatFoodState = new Eat_EatFood(this);
        foodEatenState = new Eat_FoodEaten(this);
        base.OnActivated();
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        ResetManager();
    }

    public override void ResetManager()
    {
        // Release seized canteen (Probably never used)
        if(targetCanteen != null)
        {
            targetCanteen.ReleaseSpot(Agent.gameObject);
            targetCanteen = null;
        }
        eatingCompleted = false;
    }

    public IEnumerator SimulateEating()
    {
        float duration = 3f;
        yield return new WaitForSeconds(duration);
        eatingCompleted = true;
    }
}
