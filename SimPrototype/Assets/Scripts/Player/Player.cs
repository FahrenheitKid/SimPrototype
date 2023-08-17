using System;
using System.Collections;
using System.Collections.Generic;
using SimPrototype;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement),typeof(PlayerAnimatorHandler))]
public class Player : MonoBehaviour
{
    private CustomInput _input = null;
    
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAnimatorHandler _animatorHandler;
    [field: SerializeField] private Inventory _playerInventory;
    [field: SerializeField] public int Money { get; private set; }

    #region CurrentClothing
    
    //when we update the current equipped clothing here, we update the animatorHandler accordingly
    public Clothing Shirt
    {
        get => shirt;
        private set
        {
            shirt = value;
            if(_animatorHandler != null)
                _animatorHandler.ShirtAnimator.Setup(shirt);
        }
    }

    
    public Clothing Pants
    {
        get => pants;
        private set
        {
            pants = value;
            if(_animatorHandler != null)
                _animatorHandler.PantsAnimator.Setup(pants);
        }
    }

    
    public Clothing Shoes
    {
        get => shoes;
        private set
        {
            shoes = value;
            if(_animatorHandler != null)
                _animatorHandler.ShoesAnimator.Setup(shoes);
        }
    }

    
    public Clothing Hair
    {
        get => hair;
        private set
        {
            hair = value;
            if(_animatorHandler != null)
                _animatorHandler.HairAnimator.Setup(hair);
        }
    }

    
    public Clothing Hat
    {
        get => hat;
        private set
        {
            hat = value;
            if(_animatorHandler != null)
                _animatorHandler.HatAnimator.Setup(hat);
        }
    }

    [SerializeField] private Clothing shirt;
    [SerializeField] private Clothing pants;
    [SerializeField] private Clothing shoes;
    [SerializeField] private Clothing hair;
    [SerializeField] private Clothing hat;
    #endregion
    
    public Action OnShopEnter;
    public Action OnShopExit;

    public Inventory PlayerInventory
    {
        get => _playerInventory;
        set => _playerInventory = value;
    }

    // Start is called before the first frame update
    void Awake()
    {
        Init();
        PlayerInventory = new Inventory(new List<Item>());
    }

    private void Start()
    {
        Hair = GameDatabase.Instance.ItemsDatabase.GetRandomClothing(Enums.ClothingType.Hair);
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
        if (Input.GetKeyDown(KeyCode.M))
        {
            Money += 100;
        }
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

    public void RemoveItem(Item item)
    {
        PlayerInventory.Remove(item);
    }

    public void Wear(Clothing clothing)
    {
        if (clothing == null) return;
        
        switch (clothing.ClothingType)
        {
            case Enums.ClothingType.Shirt:
                Shirt = clothing;
                break;
            case Enums.ClothingType.Pants:
                Pants = Pants;
                break;
            case Enums.ClothingType.Shoes:
                Shoes = Shoes;
                break;
            case Enums.ClothingType.Hair:
                Hair = Hair;
                break;
            case Enums.ClothingType.Hat:
                Hat = Hat;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
