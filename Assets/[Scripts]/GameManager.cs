using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Blocks _blocksScript;

    private static GameManager instance = null;
    private GameManager(){}


    public static GameManager Instance()
    {
        if (instance == null)
            instance = new GameManager();
        return instance;
    }
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Manager");
        _blocksScript = GameObject.Find("Blocks").gameObject.GetComponent<Blocks>();
    }

    public Blocks GetBlocksScript()
    {
        return _blocksScript;
    }
}
