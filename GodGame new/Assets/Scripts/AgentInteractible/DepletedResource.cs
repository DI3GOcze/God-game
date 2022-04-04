using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class DepletedResource : MonoBehaviour
{
    public DepletableResource resource;
    public float regenerationTime;
    Coroutine coroutine;

    private void OnEnable() {
        coroutine = StartCoroutine(RegenerateAfterTime());    
    }

    IEnumerator RegenerateAfterTime()
    {
        yield return new WaitForSeconds(regenerationTime);
        resource.ResetResource();
    }

    public void Regenerate()
    {
        StopCoroutine(coroutine);
        resource.ResetResource();
    }
}
