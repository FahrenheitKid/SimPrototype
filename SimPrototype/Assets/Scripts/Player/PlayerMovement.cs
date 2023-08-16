using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private CustomInput _input = null;
    private Rigidbody2D _rigidbody = null;
    [SerializeField] private Vector2 _direction = Vector2.zero;
    [SerializeField] private float _moveSpeed = 10f;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    // setup function to be called from other "Parent" scripts
    public void Setup(CustomInput customInput)
    {
        _input = customInput;
        if (customInput == null)
        {
            Debug.LogWarning("Input reference is being set to null. Normally, it should not be the case");
            return;
        }
        Init();
    }
    
    // Setup function but to be used internally
    void Init()
    {
        if (_input == null)
        {
            Debug.LogError("Input reference is null, could not initialize this object");
            return;
        }
        
        _input.Enable();
        _input.Player.Movement.performed += OnMovementPerformed;
        _input.Player.Movement.canceled += OnMovementCanceled;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _rigidbody.velocity = _direction * _moveSpeed;
    }

    void OnMovementPerformed(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
    }
    
    void OnMovementCanceled(InputAction.CallbackContext context)
    {
        _direction = Vector2.zero;
    }

    private void OnDisable()
    {
        if (_input != null)
        {
            _input.Player.Movement.performed -= OnMovementPerformed;
            _input.Player.Movement.canceled -= OnMovementCanceled;
        }
    }
}
