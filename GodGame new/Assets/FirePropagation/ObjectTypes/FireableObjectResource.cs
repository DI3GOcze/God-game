using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireableObjectResource : FireableObject
{
    [SerializeField] DepletableResource resource;

    protected override void Start()
    {
        base.Start();
        resource.OnDepleted += PutDownFire;
    }

    public override void TakeDamage(float damage)
    {
        resource.GiveFireDamage(damage);
    }
}
