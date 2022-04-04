using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Vector3 baseEuler;
    private void Awake() {
        baseEuler = transform.eulerAngles;
    }
    void Update()
    {
        if(Application.isFocused)
            transform.eulerAngles = Camera.main.transform.eulerAngles;
        else
            transform.eulerAngles = baseEuler;
    }
}
