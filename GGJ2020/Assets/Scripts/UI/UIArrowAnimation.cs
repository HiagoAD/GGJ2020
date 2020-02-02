using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIArrowAnimation : MonoBehaviour
{
    [SerializeField] Image leftArrow;
    [SerializeField] Image rightArrow;

    private bool stopLoop = false;

    private void Start()
    {
        GameManager.Instance.OnMainMenu += StartAnim;
        GameManager.Instance.OnGameStart += StopAnim;
    }

    private void StartAnim()
    {
        stopLoop = false;
        AnimLeftArrow();
    }

    private void StopAnim()
    {
        stopLoop = true;
    }

    private void AnimLeftArrow()
    {
        if (stopLoop)
            return;

        leftArrow.DOFade(.3f, .3f).OnComplete(() =>
            leftArrow.DOFade(1f, .3f).OnComplete(() =>
                AnimRightArrow()
            )
        );
    }

    private void AnimRightArrow()
    {
        if (stopLoop)
            return;

        rightArrow.DOFade(.3f, .3f).OnComplete(() =>
            rightArrow.DOFade(1f, .3f).OnComplete(() =>
                AnimLeftArrow()
            )
        );
    }

    
}
