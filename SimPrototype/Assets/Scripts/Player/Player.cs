using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement),typeof(PlayerAnimatorHandler))]
public class Player : MonoBehaviour
{
    private CustomInput _input = null;
    
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAnimatorHandler _animatorHandler;
    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    void Init()
    {
        _input = new CustomInput();
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
}
