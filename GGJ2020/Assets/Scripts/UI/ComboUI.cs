using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ComboUI : MonoBehaviour
{
    [SerializeField] Transform group;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Text value;
    [SerializeField] Text combo;

    private Color cValue;
    private Color cCombo;

    private void Awake()
    {
        cValue = value.color;
        cCombo = combo.color;
        canvasGroup.alpha = 0;
    }


    void Start()
    {
        GameManager.Instance.OnComboChanged += OnComboChanged;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            Animation();
    }

    void OnComboChanged(int value)
    {
        this.value.text = value.ToString();
    }

    private void Animation()
    {
        group.DOScale(Vector3.one * .35f, 0f);

        canvasGroup.DOFade(1, .2f).OnComplete(() => {
            group.DOScale(Vector3.one * 1.25f, 0.3f).OnComplete(() => {
                group.DOScale(Vector3.one * 1, 0.1f).OnComplete(() => {
                    //value.DOColor(Color.white, 0.1f).OnComplete(() => value.DOColor(cValue, 0.1f)).SetLoops(5);
                    canvasGroup.DOFade(.7f, .1f).OnComplete(() => canvasGroup.DOFade(1f, .1f)).SetLoops(5);
                });
            });
        });
    }
}
