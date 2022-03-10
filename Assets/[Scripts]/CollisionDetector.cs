using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (player)
        {
            player.CollisionDetectedFromChild(collision);
        }
    }
}
