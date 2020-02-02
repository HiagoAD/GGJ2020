using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    Text text;

    string value;
    bool isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        if (isPlayer)
            text.color = Color.red;
        text.text = value;
    }

    public void SetText(string value, bool isPlayer = false)
    {
        this.isPlayer = isPlayer;
        this.value = value;
    }
}
