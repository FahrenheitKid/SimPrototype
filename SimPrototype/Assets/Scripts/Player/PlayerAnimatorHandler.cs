using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static SimPrototype.Enums;
using static UtilityTools.UtilityTools;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorHandler : MonoBehaviour
{
    
    private CustomInput _input = null;
    private Animator _animator = null;
    [SerializeField] private SpriteRenderer _renderer;
    
    private Direction _currentDirection  = Direction.Down;
    
    [SerializeField] private ClothingAnimator _shirtAnimator;
    [SerializeField] private ClothingAnimator _pantsAnimator;
    [SerializeField] private ClothingAnimator _shoesAnimator;
    [SerializeField] private ClothingAnimator _hairAnimator;
    [SerializeField] private ClothingAnimator _hatAnimator;
    
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
        _renderer = GetComponent<SpriteRenderer>();
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
        
        InitializeClothingAnimators();
    }

    void InitializeClothingAnimators()
    {
        _shirtAnimator.Setup(GameDatabase.Instance.ItemsDatabase.GetRandomClothing(ClothingType.Shirt));
        //_shirtAnimator.Setup(GameDatabase.Instance.ItemsDatabase.GetClothingByID(0));
        _pantsAnimator.Setup(GameDatabase.Instance.ItemsDatabase.GetRandomClothing(ClothingType.Pants));
        _shoesAnimator.Setup(GameDatabase.Instance.ItemsDatabase.GetRandomClothing(ClothingType.Shoes));
        _hairAnimator.Setup(GameDatabase.Instance.ItemsDatabase.GetRandomClothing(ClothingType.Hair));
        _hatAnimator.Setup(GameDatabase.Instance.ItemsDatabase.GetRandomClothing(ClothingType.Hat));
    }

    void LateUpdate()
    {
        Debug.Log(_renderer.sprite.name);
        _shirtAnimator.UpdateSprite(_renderer.sprite.name);
        _pantsAnimator.UpdateSprite(_renderer.sprite.name);
        _shoesAnimator.UpdateSprite(_renderer.sprite.name);
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
