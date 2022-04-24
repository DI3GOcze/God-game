using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireableObjectBuilding : FireableObject
{
    public float maxFireHealth = 100f;
    public float fireHealth;
    public float healthRegenerationRate = 2f;

    override protected void Start() {
        base.Start();
        fireHealth = maxFireHealth;
    }

    override protected void Update() {
        base.Update();

        if(!isOnFire){
            fireHealth += healthRegenerationRate * Time.deltaTime;
        }

        if(fireHealth > maxFireHealth){
            fireHealth = maxFireHealth;
        } 
    }

    public override void TakeDamage(float damage)
    {
        fireHealth -= damage;

        if(fireHealth <= 0){
            Destroy(gameObject);
        }
    }
}
