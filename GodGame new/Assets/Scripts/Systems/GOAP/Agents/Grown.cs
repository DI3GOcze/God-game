using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GPlanner))]
// Grown has health, stats etc
public class Grown : Person
{
    public float health = (float)DefaultStatValues.HEALTH;
    public int strenght = (int)DefaultStatValues.STRENGH;
    public float strenghEfficiency => (int)DefaultStatValues.STRENGH / this.strenght;

    [SerializeField] private GPlanner _planner;
    
    [SerializeField] Professions _profession = Professions.Unemployed;
    // ..

    protected override void Start() {
        base.Start();
        World.Instance.grownList.Add(this);

        // Add components base on his selected component
        AddProfession(_profession);
        _planner.UpdateGoalsAndActions();
    }

    override protected void OnDestroy() {
        base.OnDestroy();
        World.Instance.grownList.Remove(this);
    }

    /// <summary>
    /// Only for editor OnClick functionality
    /// </summary>
    /// <param name="profession">Professions values</param>
    public void ChangeProfessionOnClick(int profession)
    {
        ChangeProfession((Professions)profession);
    }

    public void ChangeProfession(Professions profession)
    {
        // If proffesion changed
        if(_profession != profession)
        {
            RemoveProfession();
            AddProfession(profession);
            _profession = profession;
            _planner.UpdateGoalsAndActions();
        } 
    }

    public void DecreseHealth(float amount)
    {
        health -= amount;
        if(health <= 0)
            Destroy(gameObject);
    }

    private void AddProfession(Professions profession)
    {
        switch(profession)
        {
            case Professions.Woodcuter:

                gameObject.AddComponent<GoalGetWood>();
                gameObject.AddComponent<ActionGetWood>();
                break;

            case Professions.FoodGatherer:
                gameObject.AddComponent<GoalGetFood>();
                gameObject.AddComponent<ActionGetFood>();
                break;

            case Professions.Miner:
                gameObject.AddComponent<GoalGetStone>();
                gameObject.AddComponent<ActionGetStone>();
                break;

            case Professions.Unemployed:
            default:
                break;
        }
    }

    private void RemoveProfession()
    {
        List<GGoalBase> goalsToDestroy = new List<GGoalBase>();
        List<GActionBase> actionsToDestroy = new List<GActionBase>();
        
        switch (_profession)
        {
            case Professions.Woodcuter:
                goalsToDestroy.Add(GetComponent<GoalGetWood>());
                actionsToDestroy.Add(GetComponent<ActionGetWood>());
                break;

            case Professions.FoodGatherer:
                goalsToDestroy.Add(GetComponent<GoalGetFood>());
                actionsToDestroy.Add(GetComponent<ActionGetFood>());
                break;

            case Professions.Miner:
                goalsToDestroy.Add(GetComponent<GoalGetStone>());
                actionsToDestroy.Add(GetComponent<ActionGetStone>());
                break; 

            case Professions.Unemployed:
            default:
                break;
        }

        foreach (var goal in goalsToDestroy)
        {
            if(goal != null)
            {
                Destroy(goal);
            }
        }

        foreach (var action in actionsToDestroy)
        {
            if(action != null)
            {
                Destroy(action);
            }
        }
    }
    
}
