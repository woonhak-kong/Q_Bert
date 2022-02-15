using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected int m_currentPosition = 0;

    private float _offsetOfBlockPosition = 7;
    private float _offsetOfPinpadPosition = 12;

    protected bool isAlive = true;
    protected bool isJumping = false;

    private float ElapsedTime = 0;
    private float FinishTime = 15.0f;

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
        if (!isJumping)
        {
            Block block = GetBlockByIdx(m_currentPosition).m_blocks[((int)direction)];
            if (block != null)
            {
                SetPosition(block.transform);
                m_currentPosition = block.Index;
                Debug.Log("setCom!  " + m_currentPosition);
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

    private IEnumerator MoveToPosition(Transform target)
    {
        isJumping = true;

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
            //transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime);
            ElapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, position, ElapsedTime / FinishTime);

            yield return null;
            if (Vector3.Distance(transform.position, position) < 0.01f)
            {
                //Debug.Log("arrived!");
                isJumping = false;
                ElapsedTime = 0;
                if (tag == "Player")
                {
                    target.GetComponent<Block>().SetComplete();
                    //block.SetComplete();

                }
            }
           
        }
    }
}
