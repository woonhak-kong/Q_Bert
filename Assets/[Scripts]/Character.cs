using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int m_currentPosition = 0;

    private float _offsetOfPosition = 7;

    // Start is called before the first frame update
    void Start()
    {
        Block block = GetBlockByIdx(m_currentPosition);
        if (block != null)
        {
            Vector2 position = block.transform.position;
            position.y += (block.transform.localScale.y / _offsetOfPosition);
            transform.position = position;
            m_currentPosition = block.Index;
        }
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
            Vector2 position = block.transform.position;
            position.y += (block.transform.localScale.y / _offsetOfPosition);
            transform.position = position;
            m_currentPosition = block.Index;
            block.SetComplete();
        }
    }
    private Block GetBlockByIdx(int idx)
    {
        return GameManager.Instance().GetBlocksScript().GetBlocks()[m_currentPosition].GetComponent<Block>();
    }
}
