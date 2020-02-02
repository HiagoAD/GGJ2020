using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGParallax : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] renderers;

    Player player;
    List<SpriteRenderer> assets;

    private void Start()
    {
        player = GameManager.Instance.GetPlayer();
        assets = new List<SpriteRenderer>(renderers);
    }

    private void Update()
    {
        if (assets[1].bounds.min.x < player.transform.position.x && assets[1].bounds.max.x > player.transform.position.x)
        {
            return;
        } else if (assets[0].bounds.min.x < player.transform.position.x && assets[0].bounds.max.x > player.transform.position.x)
        {
            var first = assets[2];
            var middle = assets[0];
            var last = assets[1];

            first.transform.position = new Vector3(middle.bounds.min.x - (middle.bounds.size.x / 2), first.transform.position.y, first.transform.position.z);
            assets = new List<SpriteRenderer>();
            assets.Insert(0, first);
            assets.Insert(1, middle);
            assets.Insert(2, last);
        } else
        {
            var last = assets[0];
            var middle = assets[2];
            var first = assets[1];

            last.transform.position = new Vector3(middle.bounds.min.x + (middle.bounds.size.x / 2), last.transform.position.y, last.transform.position.z);
            assets = new List<SpriteRenderer>();
            assets.Insert(0, first);
            assets.Insert(1, middle);
            assets.Insert(2, last);
        }
    }
}
