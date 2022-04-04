using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float effectArea;
    public GameObject fireExplosionPrefab;

    private void OnCollisionEnter(Collision other) {
        var colliders =  Physics.OverlapSphere(transform.position, effectArea);

        foreach (var collider in colliders)
        {
            if(collider.TryGetComponent<FireGridCell>(out var fireCell))
                fireCell.SetOnFire();
        }
        var explosion = Instantiate(fireExplosionPrefab, transform.position, fireExplosionPrefab.transform.rotation);
        explosion.transform.localScale = new Vector3(effectArea,effectArea,effectArea);

        Destroy(gameObject);
    } 
}
