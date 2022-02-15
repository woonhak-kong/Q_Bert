using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    private Animator _animator;

    // Start is called before the first frame update
    public override void Start()
    {
        Block block = GetBlockByIdx(m_currentPosition);
        if (block != null)
        {
            SetPositionImediately(block.transform);
            m_currentPosition = block.Index;
        }

        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public override void MoveLeftUp()
    {
        if (!isJumping)
        {
            base.MoveLeftUp();
            _animator.SetBool("LeftUpJump", true);
        }
    }

    public override void MoveRightUp()
    {
        if (!isJumping)
        {
            base.MoveRightUp();
            _animator.SetBool("RightUpJump", true);
        }
        
    }

    public override void MoveLeftDown()
    {
        if (!isJumping)
        {
            base.MoveLeftDown();
            _animator.SetBool("LeftDownJump", true);
            
        }
        
    }

    public override void MoveRightDown()
    {
        if (!isJumping)
        {
            base.MoveRightDown();
            _animator.SetBool("RightDownJump", true);
           
        }
        
    }

    private void SetAnimatorPropertyDefault()
    {
        _animator.SetBool("RightDownJump", false);
        _animator.SetBool("LeftDownJump", false);
        _animator.SetBool("RightUpJump", false);
        _animator.SetBool("LeftUpJump", false);
    }

    protected override IEnumerator MoveToPosition(Transform target)
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
                target.GetComponent<Block>().SetComplete();
                //block.SetComplete();
                SetAnimatorPropertyDefault();

            }

        }
    }
}