using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement),typeof(PlayerAnimatorHandler))]
public class Player : MonoBehaviour
{
    private CustomInput _input = null;
    
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAnimatorHandler _animatorHandler;

    public Action OnShopEnter;

    public Action OnShopExit;
    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    void Init()
    {
        _input = new CustomInput();
        _input.Enable();
        _playerMovement = GetComponent<PlayerMovement>();
        _animatorHandler = GetComponent<PlayerAnimatorHandler>();
        
        //setup scripts
        _playerMovement.Setup(_input);
        _animatorHandler.Setup(_input);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Shop"))
        {
            OnShopEnter?.Invoke();
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Shop"))
        {
            OnShopExit?.Invoke();
        }
    }
}
