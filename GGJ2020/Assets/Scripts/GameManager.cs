using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance
    {
        get; private set;
    }

    [SerializeField]
    Player player = null;

    private void Start()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public Player GetPlayer()
    {
        return player;
    }
}
