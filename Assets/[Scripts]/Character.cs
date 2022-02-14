using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int m_currentPosition = 0;

    private float _offsetOfBlockPosition = 7;
    private float _offsetOfPinpadPosition = 12;

    // Start is called before the first frame update
    void Start()
    {
        Block block = GetBlockByIdx(m_currentPosition);
        if (block != null)
        {
            SetPosition(block.transform);
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
            SetPosition(block.transform);
            m_currentPosition = block.Index;
            block.SetComplete();
        }
    }
    private Block GetBlockByIdx(int idx)
    {
        return GameManager.Instance().GetBlocksScript().GetBlocks()[m_currentPosition].GetComponent<Block>();
    }

    private void SetPosition(Transform blockTransform)
    {
        Vector2 position = blockTransform.position;
        if (blockTransform.gameObject.GetComponent<SpinPad>() != null)
        {
            position.y += (blockTransform.localScale.y / _offsetOfPinpadPosition);
        }
        else
        {
            position.y += (blockTransform.localScale.y / _offsetOfBlockPosition);
        }
        
        transform.position = position;

    }
}
