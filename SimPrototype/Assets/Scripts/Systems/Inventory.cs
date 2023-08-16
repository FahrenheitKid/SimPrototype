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


    public void Add(Item item)
    {
        
    }

    public void Remove(Item item)
    {
        
    }
    
    public Item GetItem(Item item)
    {
        return Items.First(i => i == item);
    }
    
    public Item GetItem(string name)
    {
        return Items.First(i => i.Name == name);
    }
}
