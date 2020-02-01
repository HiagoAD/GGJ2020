using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Transform heartsArea;
    [SerializeField] GameObject heart;

    private List<Image> hearts;

    private void Awake()
    {
        hearts = new List<Image>();
        SetLife(3);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            UpdateHP(0);
        if (Input.GetKeyDown(KeyCode.W))
            UpdateHP(1);
        if (Input.GetKeyDown(KeyCode.E))
            UpdateHP(2);
        if (Input.GetKeyDown(KeyCode.R))
            UpdateHP(3);
        if (Input.GetKeyDown(KeyCode.X))
            UpdateHP(10);
    }

    public void SetLife(int value)
    {
        for (int i = 0; i < value; i++)
        {
            hearts.Add(Instantiate(heart, heartsArea).GetComponent<Image>());
        }
    }

    public void UpdateHP(int hp)
    {
        if(hp > hearts.Count)
        {
            for (int i = 0; i < hearts.Count; i++)
            {
                hearts[i].color *= Color.white;
            }

            while(hp > hearts.Count)
                hearts.Add(Instantiate(heart, heartsArea).GetComponent<Image>());
        }
        else
        {
            for (int i = 0; i < hearts.Count; i++)
            {
                if (i + 1 > hp)
                    hearts[i].color *= new Color(1, 1, 1, 0);
                else
                    hearts[i].color += new Color(0, 0, 0, 1);
            }
        }
    }
}
