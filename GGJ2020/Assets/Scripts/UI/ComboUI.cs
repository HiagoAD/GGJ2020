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
    [SerializeField] Color cLoseCombo = Color.red;

    private Color cValue;
    private Color cCombo;

    private bool freeToAdd = true;
    private int lastPoint;

    private float fadeDelay = .5f;
    private float fadeCount;
    private bool faded = true;

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
            GoodAnimation();
        if (Input.GetKeyDown(KeyCode.N))
            BadAnimation();

        if(!faded && fadeCount <= Time.time)
        {
            faded = true;
            canvasGroup.DOFade(0, .5f);
        }
    }

    void OnComboChanged(int value)
    {
        this.value.text = value.ToString();
        if (value > lastPoint)
            GoodAnimation();
        else
            BadAnimation();

        lastPoint = value;
    }

    private void GoodAnimation()
    {
        if (!freeToAdd)
            return;

        value.color = cValue;
        group.DOScale(Vector3.one * .35f, 0f);

        canvasGroup.DOFade(1, .2f).OnComplete(() => {
            group.DOScale(Vector3.one * 1.25f, 0.3f).OnComplete(() => {
                group.DOScale(Vector3.one * 1, 0.1f).OnComplete(() => {
                    canvasGroup.DOFade(.7f, .1f).OnComplete(() => canvasGroup.DOFade(1f, .1f)).SetLoops(5)
                    .OnComplete(() => { canvasGroup.alpha = 1; fadeCount = Time.time + fadeDelay; faded = false; });
                });
            });
        });
    }

    private void BadAnimation()
    {
        freeToAdd = false;
        value.color = cLoseCombo;
        group.DOScale(Vector3.one * .35f, 0f);

        canvasGroup.DOFade(1, .2f).OnComplete(() => {
            group.DOScale(Vector3.one * 1.25f, 0.3f).OnComplete(() => {
                group.DOScale(Vector3.one * 1, 0.1f).OnComplete(() => {
                    canvasGroup.DOFade(.7f, .1f).OnComplete(() => canvasGroup.DOFade(1f, .1f)).SetLoops(5)
                    .OnComplete(() => { freeToAdd = true; canvasGroup.alpha = 1; fadeCount = Time.time + fadeDelay; faded = false; });
                });
            });
        });
    }
}
