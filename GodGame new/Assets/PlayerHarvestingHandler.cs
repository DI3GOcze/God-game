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
    public float harvestingVibrationIntesity = 0.2f;
    public float harvestedVibrationIntesity = 0.9f;
    public float manaCostPerSecond = 20f;

    private float _playerHarvestingProgress = 0f;
    
    public XRRayInteractor leftHandInteractor;
    public XRRayInteractor rightHandInteractor;

    private void Start() {
        leftHandInteractor = GameObject.Find("LeftHand Controller").GetComponent<XRRayInteractor>();
        rightHandInteractor = GameObject.Find("RightHand Controller").GetComponent<XRRayInteractor>();
    }

    void Update()
    {
        if(PlayerStatManager.instance.mana >= Time.deltaTime * manaCostPerSecond)
        {
            PlayerStatManager.instance.DecreseMana(Time.deltaTime * manaCostPerSecond);
            
            leftHandInteractor.SendHapticImpulse(harvestingVibrationIntesity, Time.deltaTime * 0.05f);
            rightHandInteractor.SendHapticImpulse(harvestingVibrationIntesity, Time.deltaTime * 0.05f);

            _playerHarvestingProgress += Time.deltaTime;
            if(_playerHarvestingProgress >= playerResourceHarvestedAfterSeconds)
            {
                leftHandInteractor.SendHapticImpulse(harvestedVibrationIntesity, Time.deltaTime * 0.3f);
                rightHandInteractor.SendHapticImpulse(harvestedVibrationIntesity, Time.deltaTime * 0.3f);
                _playerHarvestingProgress= 0f;
                int amountHarvested = resource.GetResource(resourceType, 1);
                Warehouse.warehouseInvetory.AddAmmountOrAddNewItem(resourceType, amountHarvested);
            }
        }  
    }
}
