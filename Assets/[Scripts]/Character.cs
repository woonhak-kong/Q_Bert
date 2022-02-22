using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected int m_currentPosition = 0;

    protected float _offsetOfBlockPosition = 7;
    protected float _offsetOfPinpadPosition = 12;

    protected bool isAlive = true;
    protected bool isJumping = false;

    protected float ElapsedTime = 0;
    protected float FinishTime = 0.5f;

    protected Animator _animator;

    // Start is called before the first frame update
    public virtual void Start()
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

    public virtual void MoveLeftUp()
    {
        Move(Block.Direction.LEFT_UP);
    }
    public virtual void MoveRightUp()
    {
        Move(Block.Direction.RIGHT_UP);
    }

    public virtual void MoveLeftDown()
    {
        Move(Block.Direction.LEFT_DOWN);
    }
    public virtual void MoveRightDown()
    {
        Move(Block.Direction.RIGHT_DOWN);
    }
    protected void Move(Block.Direction direction)
    {
        if (!isJumping)
        {
            Block block = GetBlockByIdx(m_currentPosition).m_blocks[((int)direction)];
            if (block != null)
            {
                switch (direction)
                {
                    case Block.Direction.LEFT_UP:
                        _animator.SetBool("LeftUpJump", true);
                        break;
                    case Block.Direction.RIGHT_UP:
                        _animator.SetBool("RightUpJump", true);
                        break;
                    case Block.Direction.LEFT_DOWN:
                        _animator.SetBool("LeftDownJump", true);
                        break;
                    case Block.Direction.RIGHT_DOWN:
                        _animator.SetBool("RightDownJump", true);
                        break;
                }

                SetPosition(block.transform);
                m_currentPosition = block.Index;
                //Debug.Log("setCom!  " + m_currentPosition);
                //if (tag == "Player")
                //{
                //    block.SetComplete();
                    
                //}
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
       
        //Debug.Log(blockTransform.gameObject.name);
        StartCoroutine(MoveToPosition(blockTransform));
        //transform.position = position;

    }

    protected void SetPositionImediately(Transform blockTransform)
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

    protected virtual IEnumerator MoveToPosition(Transform target)
    {
        isJumping = true;

        Vector3 currentPosition = transform.position;
        Vector2 position = target.position;

        if (target.gameObject.GetComponent<SpinPad>() != null)
        {
            position.y += (target.localScale.y / _offsetOfPinpadPosition);
        }
        else
        {
            position.y += (target.localScale.y / _offsetOfBlockPosition);
        }


        while (isJumping)
        {
            ElapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPosition, position, (ElapsedTime / FinishTime));

            yield return null;
            if (Vector3.Distance(transform.position, position) < 0.0001f)
            {
                //Debug.Log("arrived!");
                isJumping = false;
                ElapsedTime = 0;
                SetPrivateProperties();
                if (tag == "Player")
                {
                    target.GetComponent<Block>().SetComplete();
                    //block.SetComplete();

                }
            }
           
        }
    }

    protected virtual void SetPrivateProperties()
    {
    }
}
