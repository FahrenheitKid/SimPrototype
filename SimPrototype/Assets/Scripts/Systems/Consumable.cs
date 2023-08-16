using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item, IConsumable
{
    private int healthModification;
    
    public override void Buy(Player buyer, Shop seller)
    {
        throw new System.NotImplementedException();
    }

    public override void Sell(Shop seller, Player buyer)
    {
        throw new System.NotImplementedException();
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }

    public override void Trash()
    {
        throw new System.NotImplementedException();
    }

    public void Consume(Player player)
    {
        throw new System.NotImplementedException();
    }
}
