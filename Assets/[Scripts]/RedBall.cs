using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall : Character
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("in RedBall");
        
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

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator StartAI()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(0.8f);
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
                isAlive = false;
                Destroy(gameObject);
            }
        }
    }
}
