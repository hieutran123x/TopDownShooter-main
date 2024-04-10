using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectilScript : ProjectilesScript
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
    }
}
