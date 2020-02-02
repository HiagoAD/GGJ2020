using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ComboUI : MonoBehaviour
{

    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        GameManager.Instance.OnComboChanged += OnComboChanged;
    }

    void OnComboChanged(int value)
    {
        text.text = value.ToString();
    }
}
