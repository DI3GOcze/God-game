using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using System;


/// <summary>
/// Implementation of continuous movement in VR
/// </summary>
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
        // Get direction which player is looking at
        Quaternion headDirection = Quaternion.Euler(0, _xrRig.Camera.gameObject.transform.eulerAngles.y, 0);
        
        // Based on headDirection and input from controller compute desired move direction
        Vector3 direction = headDirection * new Vector3(_inputAxis.x, 0, _inputAxis.y);

        // Move player in given direction
        characterController.Move(direction * Time.fixedDeltaTime * speed);

        // Simulation of gravity
        if(groundCollisions > 0){
            fallingSpeed = 0;
        } else {
            fallingSpeed += gravity * Time.fixedDeltaTime;   
        }

        // Aplly gravity force
        characterController.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    // Detection if player is standing on ground
    //
    private void OnTriggerEnter(Collider other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Terrain")
            groundCollisions++;
    }

    private void OnTriggerExit(Collider other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Terrain")
            groundCollisions--;
    }
    //
    // End of if player is standing on ground
}
