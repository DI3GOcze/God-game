using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFireManager : MonoBehaviour
{
    public float minTime;
    public float maxTime;

    private void Start() {
        StartCoroutine(RandomFireProcess());
    }

    IEnumerator RandomFireProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime)); 

            IgniteRandomObject();
        }
    } 

    void IgniteRandomObject()
    {
        var objects = FindObjectsOfType<FireableObjectBuilding>();
        
        if (objects.Length == 0)
            return;
        
        // Set on fire object
        var randomObject = objects[Random.Range(0, objects.Length - 1)];
        randomObject.IgniteObject();

        // Set on fire cell
        var colliders =  Physics.OverlapSphere(randomObject.transform.position, 1);
        foreach (var collider in colliders)
        {
            if(collider.TryGetComponent<FireGridCell>(out var fireCell))
                fireCell.SetOnFire();
        }
    }

}

