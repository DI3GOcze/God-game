using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item<T> {
    public T key;
    public int val;

    public Item(T key, int val)
    {
        this.key = key;
        this.val = val;
    }
}

/// <summary>
/// Inventory that can store amounts of items of amount >= 0 (cant store negative amounts)
/// </summary>
/// <typeparam name="T">Type of stored item</typeparam>
[System.Serializable]
public class Inventory<T>
{
    // Only to show amounts in Inpector
    [SerializeField] List<Item<T>> inspetorItems = new List<Item<T>>();
    public Inventory()
    {   
        Items = new InventoryDictionary();
    }
    
    [Serializable] public class InventoryDictionary : UnitySerializedDictionary<T, int> {}
    public InventoryDictionary Items;

    /// <summary>
    /// Returns true if inventory contains given item (even 0 amount)
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool HasItem(T item)
    {
        if(Items.ContainsKey(item))
            return true;
        else 
            return false;
    }

    
    /// <summary>
    /// Adds item to inventory
    /// If item already exists, it overrides the value
    /// </summary>
    /// <param name="item">Stored item</param>
    /// <param name="ammount">Amount of item</param>
    public void AddNewItem(T item, int ammount)
    {
        if(ammount < 0)
            throw new System.ArgumentException("Parameter cannot be less then 0", nameof(ammount));

        if(HasItem(item))
            Items[item] = ammount;
        else
            Items.Add(item, ammount); 
        
        UpdateInspectorData();
    }  

    
    /// <summary>
    /// If item is in inventory, adds ammount to item
    /// </summary>
    /// <param name="item">Desired item</param>
    /// <param name="ammount">Amount of item</param>
    public void AddAmmountOfItem(T item, int ammount)
    {
        if(ammount < 1)
            throw new System.ArgumentException("Parameter cannot be less then 1", nameof(ammount));

        if(HasItem(item))
            Items[item] += ammount;
        
        UpdateInspectorData();
    }    

    /// <summary>
    /// Adds ammount to item
    /// If item isnt in inventory it is added with ammount
    /// </summary>
    /// <param name="item">Desired item</param>
    /// <param name="ammount">Amount of item</param>
    public void AddAmmountOrAddNewItem(T item, int ammount)
    {
        if(ammount < 0)
            throw new System.ArgumentException("Parameter cannot be less then 1", nameof(ammount));

        if(HasItem(item))
            Items[item] += ammount;
        else   
            Items.Add(item, ammount);
    }

    
    /// <summary>
    /// Seizes ammount of item from Inventory
    /// If there is not enough left, seizes what is left 
    /// </summary>
    /// <param name="item">Desired item</param>
    /// <param name="ammount">Amount of item</param>
    /// <returns>Actual seized amount</returns>
    public int SeizeItem(T item, int ammount)
    {
        if(ammount < 1)
            throw new System.ArgumentException("Parameter cannot be less then 1", nameof(ammount));
            
        if(HasItem(item))
        {
            // If there is enough left
            if(Items[item] > ammount)
            {
                Items[item] -= ammount;
                UpdateInspectorData();
                return ammount;  
            }
            // If there is not enough left return what is left and remove item 
            else
            {
                int returnAmmount = Items[item];
                Items[item] = 0;
                UpdateInspectorData();
                return returnAmmount;
            }       
        }
    
        return 0;
    }

    /// <summary>
    /// Returns amount of stored item
    /// </summary>
    /// <param name="item">Desired item</param>
    /// <returns>Amount of given item in inventory </returns>
    public int ItemAmount(T item) 
    {
        if(HasItem(item))
        {
            return Items[item];
        }

        return 0;
    }

    
    /// <summary>
    /// Seizes ammount of item from Inventory
    /// If there is nothing left, seizes what is left and deletes item
    /// </summary>
    /// <param name="item">Desired item</param>
    /// <param name="ammount">Amount of item</param>
    /// <returns>Actual seized amount</returns>
    public int SeizeAndDeleteItemOnEmpty(T item, int ammount)
    {
        if(ammount < 1)
            throw new System.ArgumentException("Parameter cannot be less then 1", nameof(ammount));

        int seized = SeizeItem(item, ammount);
        if(Items.ContainsKey(item) && Items[item] <= 0)
            RemoveItem(item);

        return seized;
    }

    
    /// <summary>
    /// Removes item from inventory
    /// </summary>
    /// <param name="deleteThis">Desired item for deletion</param>
    public void RemoveItem(T deleteThis)
    {
        if(Items.ContainsKey(deleteThis)) 
            Items.Remove(deleteThis);
    }

    
    /// <summary>
    /// Returns true if inventory isnt empty
    /// </summary>
    /// <returns>true if Inventory is empty</returns>
    public bool IsEmpty()
    {
        return Items.Count < 1;
    }

    /// <summary>
    /// Updating data for inspector debugging purpouses
    /// </summary>
    void UpdateInspectorData()
    {
        List<Item<T>> tmp = new List<Item<T>>();
        foreach (var item in Items)
        {
            tmp.Add(new Item<T>(item.Key, item.Value));
        }    
        inspetorItems = tmp;
    }
}
