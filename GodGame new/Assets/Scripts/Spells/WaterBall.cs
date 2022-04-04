using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    public float effectArea;

    private void OnCollisionEnter(Collision other) {
        var colliders =  Physics.OverlapSphere(transform.position, effectArea);

        foreach (var collider in colliders)
        {
            if(collider.TryGetComponent<FireGridCell>(out var fireCell))
                fireCell.PutDownFire();
            else if(collider.TryGetComponent<FireableObject>(out var fireableObject))
                fireableObject.PutDownFire();
        }

        Destroy(gameObject);
    }  
}
