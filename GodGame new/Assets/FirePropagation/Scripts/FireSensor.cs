using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSensor : MonoBehaviour
{
    public delegate void SensorObjectAction(FireableObject fireableObject);
    public event SensorObjectAction OnObjectEntered;
    public event SensorObjectAction OnObjectExited;
    
    private void OnTriggerEnter(Collider other) {       
        if(other.gameObject.TryGetComponent<FireableObject>(out var fireableObject))
        {  
            OnObjectEntered?.Invoke(fireableObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.TryGetComponent<FireableObject>(out var fireableObject))
        {
            OnObjectExited?.Invoke(fireableObject);
        }
    }
}
