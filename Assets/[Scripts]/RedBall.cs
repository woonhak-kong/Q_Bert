using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall : Enemy, Observer
{
    public override void Start()
    {
        base.Start();
        _animator = transform.GetChild(0).GetComponent<Animator>();
        GameManager.Instance().AddObserver(this);
    }
    private void SetAnimatorPropertyDefault()
    {
        _animator.SetBool("RightDownJump", false);
        _animator.SetBool("LeftDownJump", false);
    }
    protected override void SetPrivateProperties()
    {
        base.SetPrivateProperties();
        SetAnimatorPropertyDefault();
        SoundManager.Instance.PlaySound(Sounds.JellyJump);

    }
    protected override void SetEnemyBehaviorWhenAIDone()
    {
        base.SetEnemyBehaviorWhenAIDone();
        int nextPosition = Random.Range(1, 3); // nerve return 3
        if (nextPosition == 1)
        {
            MoveLeftDown();
        }
        else
        {
            MoveRightDown();
        }
        GameManager.Instance().RemoveObserver(this);
        //DestroyMySelf();
    }

    public void Notify(string message)
    {
        switch (message)
        {
            case "die":
                //destory
                DestroyMySelf();
                break;
            case "freeze":
                isFreezing = true;
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
                break;
        }
    }

    protected override void FallingDown(Transform transform)
    {
        base.FallingDown(transform);
        //GameManager.Instance().RemoveObserver(this);
        SetPosition(transform, true);
    }
}
