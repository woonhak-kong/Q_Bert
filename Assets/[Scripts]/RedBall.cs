using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall : Character
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("in RedBall");
        int position = Random.Range(1, 2);
        Block block = GetBlockByIdx(position);
        if (block != null)
        {
            SetPosition(block.transform);
            m_currentPosition = block.Index;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
