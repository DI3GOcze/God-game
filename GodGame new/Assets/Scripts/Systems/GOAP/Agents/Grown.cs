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
    
    public Professions profession = Professions.Unemployed;

    protected override void Start() {
        base.Start();
        World.Instance.grownList.Add(this);

        // Add components base on his selected component
        AddProfession(profession);
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
        // If profession changed
        if(this.profession != profession)
        {
            RemoveProfession();
            AddProfession(profession);
            this.profession = profession;
            _planner.UpdateGoalsAndActions();
            
            
            // Create popup message based on selected profession
            switch(profession)
            {
                case Professions.Woodcuter:
                    PopUpManager.instance.CreatePopUp(transform.position, "Woodcuter");
                    break;

                case Professions.FoodGatherer:
                    PopUpManager.instance.CreatePopUp(transform.position, "Food gatherer");
                    break;

                case Professions.Miner:
                    PopUpManager.instance.CreatePopUp(transform.position, "Miner");
                    break;

                case Professions.Unemployed:
                default:
                    PopUpManager.instance.CreatePopUp(transform.position, "Unemloyed");
                    break;
            }
        } 
    }

    /// <summary>
    /// Decreses Grown health
    /// </summary>
    /// <param name="amount">Decresed amount</param>
    public void DecreseHealth(float amount)
    {
        health -= amount;
        if(health <= 0)
            Destroy(gameObject);
    }

    /// <summary>
    /// Adds all dependecies for given profession to gameObject
    /// </summary>
    /// <param name="profession">Selected profession</param>
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

    /// <summary>
    /// Removes all dependecies for given profession from gameObject
    /// </summary>
    /// <param name="profession">Selected profession</param>
    private void RemoveProfession()
    {
        List<GGoalBase> goalsToDestroy = new List<GGoalBase>();
        List<GActionBase> actionsToDestroy = new List<GActionBase>();
        
        switch (profession)
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
