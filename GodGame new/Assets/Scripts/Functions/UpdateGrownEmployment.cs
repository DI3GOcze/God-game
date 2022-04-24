using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
                ProfessionText.text = "Food Hatherer";
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
