using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBall : Enemy, Observer
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
        GameManager.Instance().RemoveObserver(this);
        DestroyMySelf();
    }



    public void Notify()
    {
        //destory
        DestroyMySelf();
    }
}
