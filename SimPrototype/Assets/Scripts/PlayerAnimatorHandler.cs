using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static SimPrototype.Enums;
using static UtilityTools.UtilityTools;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorHandler : MonoBehaviour
{
    
    private CustomInput _input = null;
    private Animator _animator = null;
    
    private Direction _currentDirection  = Direction.Down;
    
    #region AnimatorHashes
    
    private static readonly int AnimationSpeed = Animator.StringToHash("AnimationSpeed");
    private static readonly int UpPressed = Animator.StringToHash("UpPressed");
    private static readonly int RightPressed = Animator.StringToHash("RightPressed");
    private static readonly int LeftPressed = Animator.StringToHash("LeftPressed");
    private static readonly int DownPressed = Animator.StringToHash("DownPressed");
    
    #endregion
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
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
    
    // Like Setup function but to be used internally
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
    void Update()
    {
        
    }
    
    void OnMovementPerformed(InputAction.CallbackContext context)
    {
        Vector2 directionValue = context.ReadValue<Vector2>();
        _animator.SetFloat(AnimationSpeed,directionValue.magnitude);

        _currentDirection = GetDirectionFromVector2(directionValue);
        switch (_currentDirection)
        {
            case Direction.Up:
                _animator.SetTrigger(UpPressed);
                break;
            case Direction.Right:
                _animator.SetTrigger(RightPressed);
                break;
            case Direction.Left:
                _animator.SetTrigger(LeftPressed);
                break;
            case Direction.Down:
            default:
                _animator.SetTrigger(DownPressed);
                break;
        }
    }
    
    void OnMovementCanceled(InputAction.CallbackContext context)
    {
        //stop animation by setting the speed to zero
        _animator.SetFloat(AnimationSpeed,0);
        //we want to switch to the first frame of the current animation as is the best looking to stand still. It serves like an "idle" frame of each direction
        _animator.Play(GetAnimationNameFromDirection(_currentDirection),0,0);
    }

    private void OnDisable()
    {
        if (_input != null)
        {
            _input.Player.Movement.performed -= OnMovementPerformed;
            _input.Player.Movement.canceled -= OnMovementCanceled;
        }
    }

    public static string GetAnimationNameFromDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return "PlayerWalkUp";
                break;
            case Direction.Right:
                return "PlayerWalkRight";
                break;
            case Direction.Left:
                return "PlayerWalkLeft";
                break;
            case Direction.Down:
            default:
                return "PlayerWalkDown";
                break;
        }
    }
}
