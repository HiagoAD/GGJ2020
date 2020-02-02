using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Player player;
    public void SetPlayerScript(Player player)
    {
        this.player = player;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        player.OnPlayerAttackHit(other.gameObject);
    }
}
