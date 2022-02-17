using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public override void Start()
    {
        //Debug.Log("in Enemy");

        int position = Random.Range(1, 3); // nerve return 3
        //Debug.Log(position);
        Block block = GetBlockByIdx(position);
        if (block != null)
        {
            SetPosition(block.transform);
            m_currentPosition = block.Index;
        }

        StartCoroutine(StartAI());
    }
    protected virtual IEnumerator StartAI()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(1.0f);
            Block block = GetBlockByIdx(m_currentPosition).m_blocks[((int)Block.Direction.RIGHT_DOWN)];

            if (block != null)
            {
                int nextPosition = Random.Range(1, 3); // nerve return 3
                if (nextPosition == 1)
                {
                    MoveLeftDown();
                }
                else
                {
                    MoveRightDown();
                }
            }
            else
            {
                SetEnemyBehaviorWhenAIDone();
                break;
            }
        }
    }

    protected virtual void SetEnemyBehaviorWhenAIDone()
    {

    }
}
