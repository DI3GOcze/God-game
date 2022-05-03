using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Implementation of squezing villager to hard leads to killing him
/// Isnt used in final product... wasnt fun feature
/// </summary>
public class HoldPressureHandler : MonoBehaviour
{
    public XRRayInteractor leftHand;
    public XRRayInteractor rightHand;
    public float damageRate = 30;
    
    [SerializeField] InputDeviceCharacteristics _leftControllerCharacteristic;
    [SerializeField] InputDeviceCharacteristics _rightControllerCharacteristic;
    
    private UnityEngine.XR.InputDevice _leftController;
    private UnityEngine.XR.InputDevice _rightController;
    private Grown _grown;
    public IXRInteractable test;
    private void Start() {
        leftHand = GameObject.Find("LeftHand Controller").GetComponent<XRRayInteractor>();
        rightHand = GameObject.Find("RightHand Controller").GetComponent<XRRayInteractor>();
        _grown = GetComponent<Grown>();
    }

    private void Update() {       
        var leftHandObjects = leftHand.interactablesSelected;
        var rightHandObjects = rightHand.interactablesSelected;

        float gripValue = 0f;
 
        // Iterate throug all objects held by left hand
        if(leftHandObjects.Count > 0)
        {
            foreach (var item in leftHandObjects)
            {
                // If this object is held this hand
                if(item.transform.gameObject == this.gameObject)
                {
                    gripValue = GetGripValue(_leftController);
                    // If squezing too hard give damage to held villager
                    if(gripValue >= 0.9f)
                    {
                        _grown.DecreseHealth(damageRate * Time.deltaTime * gripValue);
                        leftHand.SendHapticImpulse(gripValue, 0.05f);
                    }
                    return;
                }    
            }
        }

        if(rightHandObjects.Count > 0)
        {
            // Iterate throug all objects held by right hand
            foreach (var item in rightHandObjects)
            {
                // If this object is held this hand
                if(item.transform.gameObject == this.gameObject)
                {
                    gripValue = GetGripValue(_rightController);
                    if(gripValue >= 0.9f)
                    {
                        _grown.DecreseHealth(damageRate * Time.deltaTime * gripValue);
                        rightHand.SendHapticImpulse(gripValue, 0.05f);
                    }
                    return;
                }   
            }
        }
    }

    /// <summary>
    /// Gets value of grip button
    /// </summary>
    /// <param name="controller">Desired controller (left or right)</param>
    /// <returns>Value of button</returns>
    float GetGripValue(UnityEngine.XR.InputDevice controller)
    {
        if(!controller.isValid)
        {
            if(controller == _rightController)
                GetController(_rightControllerCharacteristic);
            else
                GetController(_leftControllerCharacteristic);
            return 0;
        } else
        {
            controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out float gripValue);
            return gripValue;
        }
    }

    /// <summary>
    /// Gets desired input controller based on device characteristic
    /// </summary>
    /// <param name="characteristics">Controller characteristics</param>
    void GetController(InputDeviceCharacteristics characteristics)
    {
        // Set device
        var handDevice = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristics, handDevice);

        if(handDevice.Count > 0)
        {
            if(characteristics == _rightControllerCharacteristic)
                _rightController = handDevice[0];
            else
                _leftController = handDevice[0];

        }
    }
}
