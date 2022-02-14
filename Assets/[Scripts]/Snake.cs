using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy
{
  
    public bool IsHatched { get; set; }

    public override void Start()
    {
        base.Start();
        GameManager.Instance().NumOfSnake++;
        IsHatched = false;
    }

    protected override IEnumerator StartAI()
    {
        while (isAlive)
        {
            //Debug.Log("in Snake, StartAI");
            yield return new WaitForSeconds(1.0f);

            // it is not snake yet.
            if (!IsHatched)
            {
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
                    //GameManager.Instance().NumOfSnake--;
                    //Destroy(gameObject);
                }
            }
            // Hatched!! it is snake now!
            else
            {

            }
        }
    }
}
