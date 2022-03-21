using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy, Observer
{
  
    public bool IsHatched { get; set; }

    private Player _player;

    private bool _isSetToFall = false;
    private bool _isPlayerRight;

    public override void Start()
    {
        base.Start();
        GameManager.Instance().NumOfSnake++;
        IsHatched = false;
        _animator = transform.GetChild(0).GetComponent<Animator>();

        GameManager.Instance().AddObserver(this);
        _player = GameObject.Find("PlayerPosition").GetComponent<Player>();
    }

    private IEnumerator StartSnakeAI()
    {
        while (isAlive)
        {
            //Debug.Log("in Snake, StartAI");
            yield return new WaitForSeconds(1.0f);

            if (!_isSetToFall)
            {
                _isSetToFall = _player.isOnSpinPad;
                _isPlayerRight =  GameManager.Instance().GetBlocksScript().gameObject.transform.position.x < _player.transform.position.x;
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
               
            }
            // go to fall down
            else
            {
                _isSetToFall = _player.isOnSpinPad;
                float dist = float.MaxValue;
                int shortestIdx = 0;
                for (int i = 0; i < 4; i++)
                {
                    Block block = GetBlockByIdx(m_currentPosition).m_blocks[i];
                    if (block != null)
                    {
                        float distanceSpinpad = 0;
                        if (_isPlayerRight)
                            distanceSpinpad = block.DistanceFromRightSpinPad;
                        else
                            distanceSpinpad = block.DistanceFromLeftSpinPad;
                        if (dist > distanceSpinpad)
                        {
                            dist = distanceSpinpad;
                            shortestIdx = i;
                        }
                    }
                }

                if (_isPlayerRight)
                {
                    if (GetBlockByIdx(m_currentPosition).DistanceFromRightSpinPad < GetBlockByIdx(m_currentPosition)
                            .m_blocks[shortestIdx].DistanceFromRightSpinPad)
                    {
                        Move(Block.Direction.RIGHT_UP);
                    }
                }
                else
                {
                    if (GetBlockByIdx(m_currentPosition).DistanceFromLeftSpinPad < GetBlockByIdx(m_currentPosition)
                            .m_blocks[shortestIdx].DistanceFromLeftSpinPad)
                    {
                        Move(Block.Direction.LEFT_UP);
                    }
                }
                Move((Block.Direction)shortestIdx);
            }
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

    protected override void FallingDown(Transform transform)
    {
        base.FallingDown(transform);
        Debug.Log("Coilly falling");
        GameManager.Instance().RemoveObserver(this);
        SoundManager.Instance.PlaySound(Sounds.CoilFall);
        SetPosition(transform, true);
    }

    public void Notify()
    {
        //destory
        isAlive = false;
        //StopAllCoroutines();
        GameManager.Instance().NumOfSnake--;
        Destroy(gameObject);
        //DestroyMySelf();
    }
    protected override void DestroyMySelf()
    {
        GameManager.Instance().NumOfSnake--;
        GameManager.Instance().NotifyObservers();
        GameManager.Instance().RemoveAllObservers();
        GameManager.Instance().IsCoilyDead = true;
        base.DestroyMySelf();
    }
}
