using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Changes profession text in Grown GUI
/// </summary>
public class UpdateGrownEmployment : MonoBehaviour
{
    public TextMeshProUGUI ProfessionText;
    public Grown grown;

    private void Update() {
        switch(grown.profession)
        {
            case Professions.Woodcuter:
                ProfessionText.text = "Woodcuter";
                break;

            case Professions.FoodGatherer:
                ProfessionText.text = "Food Gatherer";
                break;

            case Professions.Miner:
                ProfessionText.text = "Miner";
                break;

            case Professions.Unemployed:
            default:
                ProfessionText.text = "Unemloyed";
                break;
        }
          
    }
}
