using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

/// <summary>
/// Implements animation of VR hands. In Unity is used animator and hand models from Oculus https://developer.oculus.com/documentation/unity/unity-sf-customhands/
/// </summary>
[RequireComponent(typeof(Animator))]
public class AnimateHand : MonoBehaviour
{
    UnityEngine.XR.InputDevice _device;
    [SerializeField] InputDeviceCharacteristics _controllerCharacteristic;
    [SerializeField] Animator _animator;

    private void Update() {
        if(!_device.isValid)
        {
            GetDevice();
        } else
        {
            // Set grip value for animator
            _device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out float gripValue);
            _animator.SetFloat("Grip", gripValue);

            _device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out float triggerValue);
            _animator.SetFloat("Trigger", triggerValue);
        }
    }

    void GetDevice()
    {
        // Set device
        var handDevice = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(_controllerCharacteristic, handDevice);

        if(handDevice.Count > 0)
        {
            _device = handDevice[0];
        }
    }
}
