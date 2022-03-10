using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy, Observer
{
  
    public bool IsHatched { get; set; }

    public override void Start()
    {
        base.Start();
        GameManager.Instance().NumOfSnake++;
        IsHatched = false;
        _animator = transform.GetChild(0).GetComponent<Animator>();

        GameManager.Instance().AddObserver(this);
    }

    private IEnumerator StartSnakeAI()
    {
        while (isAlive)
        {
            //Debug.Log("in Snake, StartAI");
            yield return new WaitForSeconds(1.0f);

            // it is not snake yet.
            //Block block = GetBlockByIdx(m_currentPosition).m_blocks[((int)Block.Direction.RIGHT_UP)];

            float dist = float.MaxValue;
            int shortestIdx = 0;
            for (int i = 0; i < 4; i++)
            {
                Block block = GetBlockByIdx(m_currentPosition).m_blocks[i];
                if (block != null)
                {
                    if (dist > block.DistanceFromPlayer)
                    {
                        dist = block.DistanceFromPlayer;
                        shortestIdx = i;
                    }
                }
            }

            Move((Block.Direction)shortestIdx);
            //if (block != null)
            //{
            //    int nextPosition = Random.Range(1, 3); // nerve return 3
            //    if (nextPosition == 1)
            //    {
            //        MoveLeftUp();
            //    }
            //    else
            //    {
            //        MoveRightUp();
            //    }
            //}

        }
    }


    private void SetAnimatorPropertyDefault()
    {
        _animator.SetBool("RightDownJump", false);
        _animator.SetBool("LeftDownJump", false);
        _animator.SetBool("RightUpJump", false);
        _animator.SetBool("LeftUpJump", false);
    }

    protected override void SetPrivateProperties()
    {
        base.SetPrivateProperties();
        //block.SetComplete();
        SetAnimatorPropertyDefault();

        if (IsHatched)
        {
            SoundManager.Instance.PlaySound(Sounds.CoilJump);
        }
        else
        {
            SoundManager.Instance.PlaySound(Sounds.JellyJump);
        }
    }

    protected override void SetEnemyBehaviorWhenAIDone()
    {
        base.SetEnemyBehaviorWhenAIDone();
        //GameManager.Instance().NumOfSnake--;
        //Destroy(gameObject);
        _animator.SetBool("IsHatched", true);
        IsHatched = true;
        transform.GetChild(0).GetComponent<Transform>().position = new Vector3(0.0f, 0.2f,0.0f);
        StartCoroutine(StartSnakeAI());
    }

    public void Notify()
    {
        //destory
        DestroyMySelf();
    }
}
