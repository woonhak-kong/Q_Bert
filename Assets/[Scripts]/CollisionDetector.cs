using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public Character character;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (character)
        {
            character.CollisionDetectedFromChild(collision);
        }
    }
}
