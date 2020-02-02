using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Text))]
public class UIAGameBy : MonoBehaviour
{
    Text text;
    List<string> devs;
    private bool stopLoop = false;

    private int index = 0;

    void Start()
    {
        text = GetComponent<Text>();
        devs = new List<string>();

        devs.Add("Beatriz Mendes");
        devs.Add("Catarina Macena");
        devs.Add("Hiago Dias");
        devs.Add("João Vitor Albuquerque");
        devs.Add("Nara Ferreira");
        devs.Add("Pedro Barroca");
        devs.Add("Victor Maristane");

        GameManager.Instance.OnMainMenu += StartAnim;
        GameManager.Instance.OnGameStart += StopAnim;
    }

    private void Anim()
    {
        if (stopLoop)
            return;

        if (index >= devs.Count)
            index = 0;

        text.DOText(devs[index], 0.5f).OnComplete(Anim).SetDelay(2f);
        index++;
    }

    private void StartAnim()
    {
        stopLoop = false;
        Anim();
    }

    private void StopAnim()
    {
        stopLoop = true;
    }
}
