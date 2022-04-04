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

// Invetory of items that have ammount >= 0
[System.Serializable]
public class Inventory<T>
{
    [SerializeField] List<Item<T>> inspetorItems = new List<Item<T>>();
    public Inventory()
    {   
        Items = new InventoryDictionary();
    }
    
    [Serializable] public class InventoryDictionary : UnitySerializedDictionary<T, int> {}
    public InventoryDictionary Items;

    public bool HasItem(T item)
    {
        if(Items.ContainsKey(item))
            return true;
        else 
            return false;
    }

    // Adds item to inventory
    // If item already exists, it overrides the value
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

    // If item is in inventory, adds ammount to item
    public void AddAmmountOfItem(T item, int ammount)
    {
        if(ammount < 1)
            throw new System.ArgumentException("Parameter cannot be less then 1", nameof(ammount));

        if(HasItem(item))
            Items[item] += ammount;
        
        UpdateInspectorData();
    }    

    // Adds ammount to item
    // If item isnt in inventory it is added with ammount
    public void AddAmmountOrAddNewItem(T item, int ammount)
    {
        if(ammount < 0)
            throw new System.ArgumentException("Parameter cannot be less then 1", nameof(ammount));

        if(HasItem(item))
            Items[item] += ammount;
        else   
            Items.Add(item, ammount);
    }

    // Seizes ammount of item from Inventory
    // If there is not enough left, seizes what is left 
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

    // Seizes ammount of item from Inventory
    // If there is nothing left, seizes what is left and deletes item
    public int SeizeAndDeleteItemOnEmpty(T item, int ammount)
    {
        if(ammount < 1)
            throw new System.ArgumentException("Parameter cannot be less then 1", nameof(ammount));

        int seized = SeizeItem(item, ammount);
        if(Items.ContainsKey(item) && Items[item] <= 0)
            RemoveItem(item);

        return seized;
    }

    // Removes item from inventory
    public void RemoveItem(T deleteThis)
    {
        if(Items.ContainsKey(deleteThis)) 
            Items.Remove(deleteThis);
    }

    // Returns true if Inventory is empty
    public bool IsEmpty()
    {
        return Items.Count < 1;
    }

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
