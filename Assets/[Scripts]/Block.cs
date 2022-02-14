using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum Direction
    {
        LEFT_UP = 0,
        RIGHT_UP = 1,
        LEFT_DOWN = 2,
        RIGHT_DOWN = 3
    }
    
    public Block[] m_blocks = new Block[4];
    public int Index { set; get; }

    protected bool isComplete = false;
  

    public virtual void SetComplete()
    {
        if (!isComplete)
        {
            isComplete = true;
            Sprite[] SpritesData = Resources.LoadAll<Sprite>("Sprites/qbert");
            foreach (Sprite sprite in SpritesData)
            {
                if (sprite.name == "block_yellow_1")
                {
                    GetComponent<SpriteRenderer>().sprite = sprite;
                    break;
                }
            }
        }

    }
}
