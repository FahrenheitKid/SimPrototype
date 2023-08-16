using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private SpriteRenderer _shopkeeperEmoteRenderer;
    [SerializeField] private bool _isPlayerInsideShop;
    private Tween shopkeeperEmoteFloatLoop;

    public void SetupListeners(Player player, bool on)
    {
        if (on)
        {
            player.OnShopEnter += OnPlayerEnteredShopTrigger;
            player.OnShopExit += OnPlayerExitShopTrigger;
        }
        else
        {
            player.OnShopEnter -= OnPlayerEnteredShopTrigger;
            player.OnShopExit -= OnPlayerExitShopTrigger;
           
        }
    }
    
    void OnPlayerEnteredShopTrigger()
    {
        _shopkeeperEmoteRenderer.DOFade(1, 1);
        if (shopkeeperEmoteFloatLoop == null)
        {
            shopkeeperEmoteFloatLoop = _shopkeeperEmoteRenderer.transform.DOLocalMoveY(0.5f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
        else
        {
            shopkeeperEmoteFloatLoop.Restart();
        }

        _isPlayerInsideShop = true;
    }
    
    void OnPlayerExitShopTrigger()
    {
        _shopkeeperEmoteRenderer.DOFade(0, 1);
        if (shopkeeperEmoteFloatLoop != null)
        {
            shopkeeperEmoteFloatLoop.Pause();
        }

        _isPlayerInsideShop = false;

    }

    void ShowShopButton()
    {
        
    }

    void ShowShopMenu()
    {
        
    }
}
