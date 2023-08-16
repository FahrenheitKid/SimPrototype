using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseOverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private float _scale;
    [SerializeField] private float _duration;

    private Tween animationTween;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (animationTween == null)
        {
            animationTween = transform.DOScale(new Vector3(_scale,_scale, 1f), _duration).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
        else
        {
            animationTween.Kill();
            animationTween = transform.DOScale(new Vector3(_scale,_scale, 1f), _duration).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animationTween != null)
        {
            animationTween.Kill();
            animationTween = transform.DOScale(new Vector3(1f, 1f, 1f), _duration/2f).SetEase(Ease.InOutSine);
        }
    }

    private void OnDisable()
    {
        animationTween?.Kill();
    }
}
