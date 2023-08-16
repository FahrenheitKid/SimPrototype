using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Items/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> Items;
}