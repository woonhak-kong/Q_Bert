using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBall : Enemy
{
    public override void Start()
    {
        base.Start();
        _animator = transform.GetChild(0).GetComponent<Animator>();
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

    }
}
