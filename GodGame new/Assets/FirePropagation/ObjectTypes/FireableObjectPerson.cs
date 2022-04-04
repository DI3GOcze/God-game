using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Grown))]
public class FireableObjectPerson : FireableObject
{
    [SerializeField] Grown _grown;
    
    private void Start() {
        _grown = GetComponent<Grown>();   
    }

    public override void TakeDamage(float damage)
    {
        _grown.DecreseHealth(damage);
    }
}
