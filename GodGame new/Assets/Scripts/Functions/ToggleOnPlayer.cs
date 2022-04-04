using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
