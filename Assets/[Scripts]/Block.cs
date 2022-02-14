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

    private bool isComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetComplete()
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
