using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float effectArea;
    public GameObject fireExplosionPrefab;

    public GameObject[] stonePrefabs;

    private void OnCollisionEnter(Collision other) {
        
        if(stonePrefabs.Length > 0 && other.contactCount > 0)
        {
            var randomPrefab = stonePrefabs[Random.Range(0, stonePrefabs.Length)];
            Instantiate(randomPrefab, other.contacts[0].point, randomPrefab.transform.rotation);
        }
        
        var colliders =  Physics.OverlapSphere(transform.position, effectArea);

        foreach (var collider in colliders)
        {
            if(collider.TryGetComponent<FireGridCell>(out var fireCell))
                fireCell.SetOnFire();
            else if(collider.TryGetComponent<FireableObject>(out var fireableObject))
                fireableObject.IgniteObject();
        }
        
        var explosion = Instantiate(fireExplosionPrefab, transform.position, fireExplosionPrefab.transform.rotation);
        explosion.transform.localScale = new Vector3(explosion.transform.localScale.x * effectArea, explosion.transform.localScale.y * effectArea, explosion.transform.localScale.z * effectArea);

        Destroy(gameObject);
    } 
}
