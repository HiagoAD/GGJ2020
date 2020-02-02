using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreUI : MonoBehaviour
{

    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        GameManager.Instance.OnScoreChanged += OnScoreChange;
    }

    void OnScoreChange(int value)
    {
        text.text = value.ToString();
    }
}
