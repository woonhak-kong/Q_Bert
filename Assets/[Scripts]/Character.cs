using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected int m_currentPosition = 0;

    private float _offsetOfBlockPosition = 7;
    private float _offsetOfPinpadPosition = 12;

    protected bool isAlive = true;

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
            if (tag == "Player")
            {
                block.SetComplete();
            }
        }
    }
    protected Block GetBlockByIdx(int idx)
    {
        //return GameManager.Instance().GetBlocksScript().GetBlocks()[m_currentPosition].GetComponent<Block>();
        return GameManager.Instance().GetBlocksScript().GetBlocks()[idx].GetComponent<Block>();
    }

    protected void SetPosition(Transform blockTransform)
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

        //Debug.Log(blockTransform.gameObject.name);
        transform.position = position;

    }
}
