using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private Shop _shop;
    // Start is called before the first frame update
    void Start()
    {
        _shop.SetupListeners(_player,true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        _shop.SetupListeners(_player,false);
    }
}
