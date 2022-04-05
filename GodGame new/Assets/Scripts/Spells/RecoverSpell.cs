using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverSpell : MonoBehaviour
{
    public float effectArea;

     public GameObject particlePrefab;

    private void OnCollisionEnter(Collision other) {
        var colliders =  Physics.OverlapSphere(transform.position, effectArea);

        foreach (var collider in colliders)
        {
            if(collider.TryGetComponent<DepletedResource>(out var depletedResource)){
                depletedResource.Regenerate();
                Debug.Log("regeneruju");
            }
        }

        var particle = Instantiate(particlePrefab, transform.position, particlePrefab.transform.rotation);
        particle.transform.localScale = new Vector3(particle.transform.localScale.x * effectArea, particle.transform.localScale.y * effectArea, particle.transform.localScale.z * effectArea);

        Destroy(gameObject);
    } 
}
