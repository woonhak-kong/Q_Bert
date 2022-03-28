using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player m_player;
    // Start is called before the first frame update
    void Start()
    {
        //m_player = GameObject.Find("Player").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("7"))
        {
            m_player.MoveLeftUp();

            //Debug.Log("7");
            
        }
        if (Input.GetButtonDown("9"))
        {
            m_player.MoveRightUp();
            //Debug.Log("9");

        }
        if (Input.GetButtonDown("1"))
        {
            m_player.MoveLeftDown();
            //Debug.Log("1");
        }
        if (Input.GetButtonDown("3"))
        {
            //Debug.Log("3");
            m_player.MoveRightDown();
        }

        // for debug
        if (Input.GetButtonDown("ClearGame(Debug)"))
        {
            GameManager.Instance().GameComplete();
        }
    }
}
