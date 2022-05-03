using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Toggles gameobject when player enteres or exits
/// </summary>
public class ToggleOnPlayer : MonoBehaviour
{
    public GameObject ToggledObject; 

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
            ToggledObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
            ToggledObject.SetActive(false);
    }
}
