using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using System;

public class ContinuousMovement : MonoBehaviour
{
    public float speed = 30f;
    public XRNode inputSource;
    public int groundCollisions = 0;
    public LayerMask terrainLayer;
    CharacterController characterController;
    Vector2 _inputAxis;
    XROrigin _xrRig;
    float gravity;
    float fallingSpeed = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();    
        _xrRig = GetComponent<XROrigin>();
        gravity = Physics.gravity.y;
    }

    private void Update() {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out _inputAxis);
    }

    void FixedUpdate()
    {
        Quaternion headDirection = Quaternion.Euler(0, _xrRig.Camera.gameObject.transform.eulerAngles.y, 0);
        
        Vector3 direction = headDirection * new Vector3(_inputAxis.x, 0, _inputAxis.y);

        characterController.Move(direction * Time.fixedDeltaTime * speed);

        if(groundCollisions > 0){
            fallingSpeed = 0;
        } else {
            fallingSpeed += gravity * Time.fixedDeltaTime;   
        }

        characterController.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Terrain")
            groundCollisions++;
    }

    private void OnTriggerExit(Collider other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Terrain")
            groundCollisions--;
    }
}
