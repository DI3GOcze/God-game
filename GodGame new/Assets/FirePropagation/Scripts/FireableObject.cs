using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class FireableObject : MonoBehaviour
{
    [SerializeField] GameObject _fireParticlePrefab;
    [SerializeField] GameObject _fireModelSpawnPoint;
    [SerializeField] float igniteTreshold = 100f;
    [SerializeField] float temperatureDecreaseRate = 20f;
    [SerializeField] float temperatureIncreaseRate = 40f;
    [SerializeField] float _temperature;
    public delegate void ObjectDiabledAction(FireableObject fireableObject);
    public event ObjectDiabledAction OnDisabled;
    /// <summary>
    /// Damage taken per second when on fire
    /// </summary>
    public float onFireDamage = 5;
    [SerializeField] private bool _isOnFire = false;
    public bool isOnFire 
    {
        get { return _isOnFire; }
        set 
        {
            if(_isOnFire == value)
                return; 

            _isOnFire = value;

            if(value == true)
            {
                CrateFireModel();
            }
            else // value == false
            {
                DestroyFireModel();
            }
        }
    }
    public bool isDead = false;
    public int isInIgnitedCells = 0;
    protected virtual void Start() {
        CoolDownTemperature();
    }

    abstract public void TakeDamage(float damage);

    public void CoolDownTemperature()
    {
        _temperature = 0;
        isOnFire = false;
    }

    /// <summary>
    /// If object is in ignited cell too long it catches on fire it self
    /// </summary>
    public void IncreaseTemperature(float amount)
    {
        _temperature += amount;

        // If the temperature exceeded ignite treshold
        if(_temperature >= igniteTreshold)
        {
            _temperature = igniteTreshold;

            isOnFire = true;
        }
    }

    public void DecreaseTemperature(float amount)
    {
        _temperature -= amount;

        // If the temperature decreases enough, put down fire
        if(_temperature <= 0)
        {
            _temperature = 0;

            isOnFire = false;
        }
    }

    protected void CrateFireModel()
    {
        Instantiate(_fireParticlePrefab, _fireModelSpawnPoint.transform.position, _fireParticlePrefab.transform.rotation, _fireModelSpawnPoint.transform);
    }

    /// <summary>
    /// Destroyes all objects in _fireModelSpawnPoint object
    /// </summary>
    protected void DestroyFireModel()
    {
        foreach (Transform item in _fireModelSpawnPoint.transform)
        {
            Destroy(item.gameObject);
        }
    }

    /// <summary>
    /// Instantly sets the object on fire
    /// </summary>
    public void IgniteObject()
    {
        _temperature = igniteTreshold;
        isOnFire = true;
    }

    virtual protected void Update() {
        
        // If the object is on fire, it shloud take damage
        if(isOnFire)
        {
            TakeDamage(onFireDamage * Time.deltaTime);           
        }    

        // If object is in on fire cell, temperature should increase,
        // othervise temperature shoul decrease over time.
        if(isInIgnitedCells > 0)
            IncreaseTemperature(temperatureIncreaseRate * Time.deltaTime);
        else
            DecreaseTemperature(temperatureDecreaseRate * Time.deltaTime);
    }

    private void OnDisable() {
        OnDisabled?.Invoke(this);
    }

    private void OnDestroy() {
        OnDisabled?.Invoke(this);
    }
}
