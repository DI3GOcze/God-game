using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerHarvestingHandler : MonoBehaviour
{
    public AgentInteractibleBase resource;
    public ResourceTypes resourceType;
    public float playerResourceHarvestedAfterSeconds = 1f;
    private float harvestingVibrationIntesity = 0.2f;
    private float harvestedVibrationIntesity = 0.9f;
    private float manaCostPerSecond = 10f;
    private float _playerHarvestingProgress = 0f;


    
    public XRRayInteractor leftHandInteractor;
    public XRRayInteractor rightHandInteractor;

    public GameObject popupSpawnPoint;
    private Transform _playerView;

    private void Start() {
         _playerView = Camera.main.transform;
        leftHandInteractor = GameObject.Find("LeftHand Controller").GetComponent<XRRayInteractor>();
        rightHandInteractor = GameObject.Find("RightHand Controller").GetComponent<XRRayInteractor>();
    }

    void Update()
    {
        if(PlayerStatManager.instance.mana >= Time.deltaTime * manaCostPerSecond)
        {
            PlayerStatManager.instance.DecreseMana(Time.deltaTime * manaCostPerSecond);
            
            leftHandInteractor.SendHapticImpulse(harvestingVibrationIntesity, 0.05f);
            rightHandInteractor.SendHapticImpulse(harvestingVibrationIntesity, 0.05f);

            _playerHarvestingProgress += Time.deltaTime;
            if(_playerHarvestingProgress >= playerResourceHarvestedAfterSeconds)
            {
                 leftHandInteractor.SendHapticImpulse(harvestedVibrationIntesity, 0.3f);
                rightHandInteractor.SendHapticImpulse(harvestedVibrationIntesity, 0.3f);
                _playerHarvestingProgress= 0f;
                int amountHarvested = resource.GetResource(resourceType, 1);

                string text = "";
                if(resourceType == ResourceTypes.WOOD){
                    text = "+" + amountHarvested.ToString() + " wood";
                } else if(resourceType == ResourceTypes.STONE){
                    text = "+" + amountHarvested.ToString() + " stone";
                } else if(resourceType == ResourceTypes.FOOD){
                    text = "+" + amountHarvested.ToString() + " food";
                }
                var popup = PopUpManager.instance.CreatePopUp(popupSpawnPoint.transform.position, text);
                
                Warehouse.warehouseInvetory.AddAmmountOrAddNewItem(resourceType, amountHarvested);
            }
        }  
    }
}
