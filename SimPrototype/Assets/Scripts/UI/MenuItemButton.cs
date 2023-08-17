using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuItemButton : MonoBehaviour
{
    
    public Item ButtonItem
    {
        get => _buttonItem;
        private set => _buttonItem = value;
    }

    [SerializeField] private Image _image;

    [SerializeField] private Item _buttonItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setup(Item pItem)
    {
        ButtonItem = pItem;

        _image.sprite = ButtonItem.Icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddListenerOnClick(UnityAction buttonAction)
    {
        Button button = GetComponent<Button>();
        if(buttonAction == null || button == null) return;
        
        button.onClick.AddListener(buttonAction);
        
    }
}
