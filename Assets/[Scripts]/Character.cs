using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int m_currentPosition = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void MoveLeftUp()
    {
        Move(Block.Direction.LEFT_UP);
    }
    public void MoveRightUp()
    {
        Move(Block.Direction.RIGHT_UP);
    }

    public void MoveLeftDown()
    {
        Move(Block.Direction.LEFT_DOWN);
    }
    public void MoveRightDown()
    {
        Move(Block.Direction.RIGHT_DOWN);
    }
    private void Move(Block.Direction direction)
    {
        Block block = GetBlockByIdx(m_currentPosition).m_blocks[((int)direction)];
        if (block != null)
        {
            transform.position = block.transform.position;
            m_currentPosition = block.Index;
        }
    }
    private Block GetBlockByIdx(int idx)
    {
        return GameManager.Instance().GetBlocksScript().GetBlocks()[m_currentPosition].GetComponent<Block>();
    }
}
