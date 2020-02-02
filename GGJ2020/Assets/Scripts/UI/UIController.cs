using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Transform heartsArea;
    [SerializeField] GameObject heart;

    [Header("Canvas groups")]
    [SerializeField] CanvasGroup mainMenu;
    [SerializeField] CanvasGroup gameUI;
    [SerializeField] CanvasGroup leaderBoard;

    private List<Image> hearts;

    private void Awake()
    {
        hearts = new List<Image>();
    }

    private void Start()
    {
        UpdateHP(GameManager.Instance.maxHp);
        GameManager.Instance.PlayerHitted += UpdateHP;
        GameManager.Instance.OnRestartGame += Restart;
        GameManager.Instance.OnMainMenu += MainMenu;
        GameManager.Instance.OnGameStart += StartGame;
        GameManager.Instance.OnLeaderBoard += LeaderBoard;
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

    private void Restart()
    {
        UpdateHP(GameManager.Instance.maxHp);
    }

    private void MainMenu()
    {
        mainMenu.alpha = 1;
        mainMenu.blocksRaycasts = true;
        gameUI.alpha = 0;
        gameUI.blocksRaycasts = false;
        leaderBoard.alpha = 0;
        leaderBoard.blocksRaycasts = false;
    }

    private void StartGame()
    {
        mainMenu.alpha = 0;
        mainMenu.blocksRaycasts = false;
        gameUI.alpha = 1;
        gameUI.blocksRaycasts = true;
        leaderBoard.alpha = 0;
        leaderBoard.blocksRaycasts = false;
    }

    private void LeaderBoard()
    {
        mainMenu.alpha = 0;
        mainMenu.blocksRaycasts = false;
        gameUI.alpha = 0;
        gameUI.blocksRaycasts = true;
        leaderBoard.alpha = 1;
        leaderBoard.blocksRaycasts = true;
    }
}
