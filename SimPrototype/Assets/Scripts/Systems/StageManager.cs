using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private Shop _shop;
    [SerializeField] private ShopUI _shopUI;
    [SerializeField] private InventoryUI _inventoryUI;
    // Start is called before the first frame update
    void Start()
    {
        _shop.SetupListeners(_player,true);
        _shopUI.Setup( new Inventory(GameDatabase.Instance.ItemsDatabase.GetAllItems()),_player);
        _inventoryUI.Setup(_player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    private void OnDisable()
    {
        _shop.SetupListeners(_player,false);
    }
}
