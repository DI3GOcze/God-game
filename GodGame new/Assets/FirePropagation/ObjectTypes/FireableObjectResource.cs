using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireableObjectResource : FireableObject
{
    [SerializeField] DepletableResource resource;

    protected override void Start()
    {
        base.Start();
        resource.OnDepleted += CoolDownTemperature;
    }

    public override void TakeDamage(float damage)
    {
        resource.GiveFireDamage(damage);
    }
}
