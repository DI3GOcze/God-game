using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// World is used primary for keeping references to all types of objects
/// Thanks to this caching of references, searching for objects of type in scene is a lot faster
/// </summary>
public sealed class World {
    private static readonly World _instance = new World();
    public List<Warehouse> warehouses { get; private set; } = new List<Warehouse>();
    public List<Canteen> canteens { get; private set; } = new List<Canteen>();
    public List<Tent> tents { get; private set; } = new List<Tent>();
    public List<TreeResource> trees { get; private set; } = new List<TreeResource>();
    public List<StoneResource> stones { get; private set; } = new List<StoneResource>();
    public List<BerriesResource> berries { get; private set; } = new List<BerriesResource>();
    public List<CampFire> campFires { get; private set; } = new List<CampFire>();
    public List<Grown> grownList { get; private set; } = new List<Grown>();
    public HashSet<WorldStates> worldState { get; private set; } = new HashSet<WorldStates>();
    public TimesOfDay timeOfDay {get; private set; } = TimesOfDay.Morning;

    // Singleton
    static World() {
    }
    private World() {
    }
    public static World Instance {

        get { return _instance; }
    }

    /// <summary>
    /// Returns count of resources with free spots for agents
    /// </summary>
    /// <typeparam name="T">Type of desired resource</typeparam>
    /// <returns>Amount of resources with free spots</returns>
    public int CountOfFreeResource<T>() where T : AgentInteractibleBase
    {
        if(typeof(T).Equals(typeof(Warehouse)))
            return warehouses.FindAll(x => x.FreeSpots > 0).Count;
        else if(typeof(T).Equals(typeof(Canteen)))
            return canteens.FindAll(x => x.FreeSpots > 0).Count;
        else if(typeof(T).Equals(typeof(TreeResource)))
            return trees.FindAll(x => x.FreeSpots > 0).Count;
        else if(typeof(T).Equals(typeof(StoneResource)))
            return stones.FindAll(x => x.FreeSpots > 0).Count;
        else if(typeof(T).Equals(typeof(BerriesResource)))
            return berries.FindAll(x => x.FreeSpots > 0).Count;
        else 
            return 0;  
    }

    /// <summary>
    /// Returns all resources that have free spots of type T
    /// </summary>
    /// <typeparam name="T">Type of desired resource</typeparam>
    /// <returns>List of free resources</returns>
    public List<T> GetFreeResource<T>() where T : AgentInteractibleBase
    {
        if(typeof(T).Equals(typeof(Warehouse)))
            return warehouses.FindAll(x => x.FreeSpots > 0).Cast<T>().ToList();
        else if(typeof(T).Equals(typeof(Canteen)))
            return canteens.FindAll(x => x.FreeSpots > 0).Cast<T>().ToList();
        else if(typeof(T).Equals(typeof(TreeResource)))
            return trees.FindAll(x => x.FreeSpots > 0).Cast<T>().ToList();
        else if(typeof(T).Equals(typeof(StoneResource)))
            return stones.FindAll(x => x.FreeSpots > 0).Cast<T>().ToList();
        else if(typeof(T).Equals(typeof(BerriesResource)))
            return berries.FindAll(x => x.FreeSpots > 0).Cast<T>().ToList();
        else if(typeof(T).Equals(typeof(Tent)))
            return tents.FindAll(x => x.FreeSpots > 0).Cast<T>().ToList();
        else 
            return new List<T>();  
    }

    
    /// <summary>
    /// Adds new instance of resource to inventory
    /// </summary>
    /// <param name="instance">Added resource</param>
    public void AddNewResourceInstance(AgentInteractibleBase instance)
    {
        if(instance is TreeResource)
            trees.Add((TreeResource)instance);
        else if(instance is StoneResource)
            stones.Add((StoneResource)instance);
        else if(instance is Warehouse)
            warehouses.Add((Warehouse)instance);
        else if(instance is Canteen)
            canteens.Add((Canteen)instance);
        else if(instance is BerriesResource)
            berries.Add((BerriesResource)instance);
        else if(instance is Tent)
            tents.Add((Tent)instance);
    }

    /// <summary>
    /// Removes given resource from references
    /// </summary>
    /// <param name="instance">Desired resource</param>
    public void RemoveResourceInstance(AgentInteractibleBase instance){
        if(instance is TreeResource)
            trees.Remove((TreeResource)instance);
        else if(instance is StoneResource)
            stones.Remove((StoneResource)instance);
        else if(instance is Warehouse)
            warehouses.Remove((Warehouse)instance);
        else if(instance is Canteen)
            canteens.Remove((Canteen)instance);
        else if(instance is BerriesResource)
            berries.Remove((BerriesResource)instance);
        else if(instance is Tent)
            tents.Remove((Tent)instance);
    }

    /// <summary>
    /// Changes valus of day time
    /// </summary>
    /// <param name="timeOfDay">Desired time of day</param>
    public void ChangeTimeOfDay(TimesOfDay timeOfDay)
    {
        this.timeOfDay = timeOfDay;
    }

    /// <summary>
    /// Adds world state
    /// </summary>
    /// <param name="worldState">Desired world state</param>
    public void AddStateToWorldState(WorldStates worldState)
    {
        this.worldState.Add(worldState);
    }

    /// <summary>
    /// Returns if given worldstate if free
    /// </summary>
    /// <param name="worldState">Asked world state</param>
    /// <returns>True if world state is set</returns>
    public bool IsWorldStateSet(WorldStates worldState)
    {
        return this.worldState.Contains(worldState);
    }

    /// <summary>
    /// Removes world state
    /// </summary>
    /// <param name="worldState">Desired world state</param>
    public void RemoveStateFromWorldState(WorldStates worldState)
    {
        this.worldState.Remove(worldState);
    }
}
