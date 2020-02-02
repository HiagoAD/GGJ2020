using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UILeaderBoard : MonoBehaviour
{
    [SerializeField] GameObject main;
    [SerializeField] Text txthighScore;

    List<GameObject> names;
    List<GameObject> scores;

    List<string> sNames;
    List<int> iScores;


    [SerializeField] GameObject goNames;
    [SerializeField] GameObject goScore;

    private void Awake()
    {
        names = new List<GameObject>();
        scores = new List<GameObject>();

        sNames = new List<string>();
        iScores = new List<int>();

        sNames.Add("PLAT@0");
        sNames.Add("WICK_BOY");
        sNames.Add("R0MEO");
        sNames.Add("JULI3T");
        sNames.Add("IMBATMAN");
        sNames.Add("SHERAN");
        sNames.Add("AD3L3");
        sNames.Add("T.STARK");
        sNames.Add("TIMMAN");
        sNames.Add("THEQUINN");
        sNames.Add("LILYFOREVA");
        sNames.Add("PIGLET");

        iScores.Add(500);
        iScores.Add(200);
        iScores.Add(100);
        iScores.Add(90);
        iScores.Add(75);
        iScores.Add(60);
        iScores.Add(50);
        iScores.Add(40);
        iScores.Add(35);
        iScores.Add(20);
        iScores.Add(10);
        iScores.Add(10);
    }

    private void Start()
    {
        GameManager.Instance.OnLeaderBoard += AddLeaders;
    }

    public void AddLeaders()
    {
        Clear();
        int playerScore = PlayerPrefs.GetInt("Score", 0);

        txthighScore.DOText(playerScore.ToString(), .5f);

        for (int i = 0; i < sNames.Count; i++)
        {
            if(iScores[i] < playerScore)
            {
                var myText = Instantiate(main, goNames.transform);
                var myScore = Instantiate(main, goScore.transform);

                myText.GetComponent<UIText>().SetText("PLAYER", true);
                myScore.GetComponent<UIText>().SetText(playerScore.ToString(), true);

                names.Add(myText);
                scores.Add(myScore);
            }

            var text = Instantiate(main, goNames.transform);
            var score = Instantiate(main, goScore.transform);

            text.GetComponent<UIText>().SetText(sNames[i]);
            score.GetComponent<UIText>().SetText(iScores[i].ToString());

            names.Add(text);
            scores.Add(score);
        }
    }

    private void Clear()
    {
        foreach (var name in names)
        {
            Destroy(name);
        }

        names.Clear();

        foreach (var score in scores)
        {
            Destroy(score);
        }

        scores.Clear();
    }

}
