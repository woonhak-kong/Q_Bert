using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public GameObject speachBubble;

    private bool isDeadFalling = false;

    // Start is called before the first frame update
    public override void Start()
    {
        Block block = GetBlockByIdx(m_currentPosition);
        if (block != null)
        {
            SetPositionImediately(block.transform);
            m_currentPosition = block.Index;
            GameManager.Instance().SetDistanceBetweenPlayerAndBlocks();
        }
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    //public override void MoveLeftUp()
    //{
    //    if (!isJumping)
    //    {
    //        base.MoveLeftUp();
    //        _animator.SetBool("LeftUpJump", true);
    //    }
    //}

    //public override void MoveRightUp()
    //{
    //    if (!isJumping)
    //    {
    //        base.MoveRightUp();
    //        _animator.SetBool("RightUpJump", true);
    //    }
        
    //}

    //public override void MoveLeftDown()
    //{
    //    if (!isJumping)
    //    {
    //        base.MoveLeftDown();
    //        _animator.SetBool("LeftDownJump", true);
            
    //    }
        
    //}

    //public override void MoveRightDown()
    //{
    //    if (!isJumping)
    //    {
    //        base.MoveRightDown();
    //        _animator.SetBool("RightDownJump", true);
           
    //    }
        
    //}

    private void SetAnimatorPropertyDefault()
    {
        _animator.SetBool("RightDownJump", false);
        _animator.SetBool("LeftDownJump", false);
        _animator.SetBool("RightUpJump", false);
        _animator.SetBool("LeftUpJump", false);
    }

    //protected override IEnumerator MoveToPosition(Transform target)
    //{
    //    isJumping = true;

    //    Vector2 position = target.position;
    //    if (target.gameObject.GetComponent<SpinPad>() != null)
    //    {
    //        position.y += (target.localScale.y / _offsetOfPinpadPosition);
    //    }
    //    else
    //    {
    //        position.y += (target.localScale.y / _offsetOfBlockPosition);
    //    }

    //    Vector3 currentPosition = transform.position;

    //    while (isJumping)
    //    {
    //        ElapsedTime += Time.deltaTime;
    //        transform.position = Vector3.Lerp(currentPosition, position, (ElapsedTime / FinishTime));

    //        yield return null;
    //        if (Vector3.Distance(transform.position, position) < 0.0001f)
    //        {
    //            //Debug.Log("arrived!");
    //            isJumping = false;
    //            ElapsedTime = 0;
                

    //        }

    //    }
    //}

    protected override void SetPrivateProperties()
    {
        base.SetPrivateProperties();
        //block.SetComplete();
        SetAnimatorPropertyDefault();
        if (isJumping)
        {
            SoundManager.Instance.PlaySound(Sounds.QbertJump);
        }

        GameManager.Instance().SetDistanceBetweenPlayerAndBlocks();
    }

    public void CollisionDetectedFromChild(Collider2D col)
    {
        if (isOnSpinPad)
            return;
        if (!isAlive)
            return;

        if (col.tag == "Enemy")
        {
            speachBubble.SetActive(true);
            SoundManager.Instance.PlaySound(Sounds.Speech1, 1.0f);
            Time.timeScale = 0.0f;
            StopAllCoroutines();

            Restart();

        }
    }

    // after collision death, restart game, reduce life, destroy all characters
    IEnumerator RestartFromDeath()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        //Debug.Log("Restart!");
        Time.timeScale = 1.0f;
        GameManager.Instance().PlayerDead();
        speachBubble.SetActive(false);
        if (GameManager.Instance().GetLife() >= 0)
        {
            if (isDeadFalling)
            {
                SetPositionImediately(GetBlockByIdx(0).transform);
                m_currentPosition = 0;
                m_previousIdx = 0;
                isDeadFalling = false;
            }
            else
            {
                SetPositionImediately(GetBlockByIdx(m_previousIdx).transform);
                m_currentPosition = m_previousIdx;
            }
            isJumping = false;
            isAlive = true;
            ArrivedAtDestination();
        }
        else
        {
            StopAllCoroutines();
            SetPositionImediatelyOutOfScreen();
        }
        transform.GetChild(0).GetComponent<Renderer>().sortingLayerName = "Player";
        
        //destroy all characters
        GameManager.Instance().NotifyObservers();
        GameManager.Instance().RemoveAllObservers();
    }

    protected override void FallingDown(Transform transform)
    {
        base.FallingDown(transform);
        isDeadFalling = true;
        Restart();
        SoundManager.Instance.PlaySound(Sounds.Fall);
        isAlive = false;
        SetPosition(transform, true);
    }

    public void Restart()
    {
        StartCoroutine(RestartFromDeath());
    }
}
