using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDatabase : Singleton<GameDatabase>
{
    [field: SerializeField] public ItemDatabase ItemsDatabase { get; private set; }
}
