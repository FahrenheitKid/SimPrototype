using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private BoxCollider2D _shopCollider;
    [SerializeField] private SpriteRenderer _shopkeeperEmoteRenderer;
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
    }
    
    void OnPlayerExitShopTrigger()
    {
        _shopkeeperEmoteRenderer.DOFade(0, 1);
        if (shopkeeperEmoteFloatLoop != null)
        {
            shopkeeperEmoteFloatLoop.Pause();
        }
        
    }
}
