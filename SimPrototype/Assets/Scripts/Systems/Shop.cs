using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    
    [SerializeField] private SpriteRenderer _shopkeeperEmoteRenderer;
    [SerializeField] private bool _isPlayerInsideShop;
    [SerializeField] private Button shopButton;
    private Tween shopkeeperEmoteFloatLoop;
    private Tween shopButtonFadeTween;

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
        ShowShopButton(_isPlayerInsideShop);
    }
    
    void OnPlayerExitShopTrigger()
    {
        _shopkeeperEmoteRenderer.DOFade(0, 1);
        if (shopkeeperEmoteFloatLoop != null)
        {
            shopkeeperEmoteFloatLoop.Pause();
        }

        _isPlayerInsideShop = false;
        ShowShopButton(_isPlayerInsideShop);

    }

    void ShowShopButton(bool on)
    {
        CanvasGroup shopButtonGroup = shopButton.GetComponent<CanvasGroup>();
        if (shopButtonGroup == null) return;


        shopButtonGroup.interactable = on;
        shopButtonGroup.blocksRaycasts = on;

        if (shopButtonFadeTween == null)
        {
           shopButtonFadeTween = shopButtonGroup.DOFade(on ? 1f : 0, 1f);
        }
        else
        {
            shopButtonFadeTween.Kill();
            shopButtonFadeTween = shopButtonGroup.DOFade(on ? 1f : 0, 1f);
        }


    }

}
