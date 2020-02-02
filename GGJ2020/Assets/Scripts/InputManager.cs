using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Player player = null;
    private bool playerAlive = true;

    private void Start()
    {
        player = GameManager.Instance.GetPlayer();
        GameManager.Instance.OnRestartGame += Restart;
        GameManager.Instance.OnGameOver += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(playerAlive)
                player.Attack(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (playerAlive)
                player.Attack(1);
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.OnRestartGame();
        }
    }

    private void GameOver()
    {
        playerAlive = false;
    }

    private void Restart()
    {
        playerAlive = true;
    }

}
