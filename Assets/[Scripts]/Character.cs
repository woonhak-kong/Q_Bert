using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private Blocks _blocksScript;

    private int m_currentPosition = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Character");
        _blocksScript = GameManager.Instance().GetBlocksScript();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("7"))
        {
            Debug.Log("7");
        }
        if (Input.GetButtonDown("9"))
        {
            Debug.Log("9");
        }
        if (Input.GetButtonDown("1"))
        {
            Debug.Log("1");
        }
        if (Input.GetButtonDown("3"))
        {
            Debug.Log("3");
        }

    }
}
