using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimPrototype;
using UnityEngine;

[Serializable]
public class Inventory
{
    [field: SerializeField] public List<Item> Items { get; private set; }
    
    public Inventory(List<Item> items)
    {
        Items = items;
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public bool Remove(Item item)
    {
        if (item == null) return false;
        bool success = Items.Remove(item);
        if (!success)
        {
            int i = Items.FindIndex(x => x.ID == item.ID || (x.Name == item.Name && !string.IsNullOrEmpty(x.Name)));
            if (i >= 0 && i < Items.Count)
            {
                Items.RemoveAt(i);
                return true;
            }
            
        }

        return success;

    }

    public bool HasItem(Item item, bool identical = false)
    {
        if (item == null) return false;
        
        // in case you want the exact object or just any instance of it"
        if (identical)
        {
            return Items.Contains(item);
        }
        else
        {
            return Items.Any(x => x == item || x.ID == item.ID || x.Name == item.Name);
        }
    }
    
    public Item GetItem(Item item, bool identical = false)
    {
        if(item == null)  return null;

        if (identical)
        {
            
            return Items.First(i => i == item);
        }
        else
        {
            return Items.First(i => i == item || i.ID == item.ID || i.Name == item.Name);
        }
    }
    public Item GetItem(int id)
    {
        if(id < 0 || id > GameDatabase.Instance.ItemsDatabase.Items.Count)  return null;
            return Items.First(i => i.ID == id);
    }
    
    public Item GetItem(string name)
    {
        if(string.IsNullOrEmpty(name))  return null;
            return Items.First(i => i.Name == name);
    }
}
