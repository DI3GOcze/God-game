using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverSpell : MonoBehaviour
{
    public float effectArea;

    private void OnCollisionEnter(Collision other) {
        var colliders =  Physics.OverlapSphere(transform.position, effectArea);

        foreach (var collider in colliders)
        {
            if(collider.TryGetComponent<DepletedResource>(out var depletedResource))
                depletedResource.Regenerate();
        }

        Destroy(gameObject);
    } 
}
