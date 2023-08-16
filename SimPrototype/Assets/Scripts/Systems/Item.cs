using System;
using System.Collections;
using System.Collections.Generic;
using SimPrototype;
using UnityEngine;

public abstract class Item
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Enums.ItemType ItemType { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public int SellPriceModifier { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    

    public abstract void Buy();
    public abstract void Sell();
    public abstract void Use();
    public abstract void Trash();

}

public interface IConsumable
{
    public void Consume(Player player);
}

public interface IWearable
{
    public void Wear(Player player);
}
