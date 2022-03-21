using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected float m_Speed;

    public override void Start()
    {
        //Debug.Log("in Enemy");

        int position = Random.Range(1, 3); // nerve return 3
        //Debug.Log(position);
        Block block = GetBlockByIdx(position);
        if (block != null)
        {
            transform.position = block.transform.position + Vector3.up * 4;
            SetPosition(block.transform);
            m_currentPosition = block.Index;
        }

        m_Speed = Random.Range(0.6f, 1.0f);
        StartCoroutine(StartAI());
    }
    protected virtual IEnumerator StartAI()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(m_Speed);
            if (isFreezing)
            {
                yield return new WaitForSeconds(4.0f);
                isFreezing = false;
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            }
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
