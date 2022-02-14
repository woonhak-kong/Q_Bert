using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject redball;

    [SerializeField]
    private Blocks _blocksScript;

    private static GameManager instance = null;

    private Coroutine _enemySpawnCoroutine = null;

    private bool _isPlayingGame = false;



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
        _blocksScript = GameObject.Find("Blocks").gameObject.GetComponent<Blocks>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _isPlayingGame = true;
        //_enemySpawnCoroutine = EnemySpawn();
        StartCoroutine(EnemySpawn());
    }

    public Blocks GetBlocksScript()
    {
        return _blocksScript;
    }

    private IEnumerator EnemySpawn()
    {
        while (_isPlayingGame)
        {
            yield return new WaitForSeconds(2.0f);
            // todo 
            // selection of enemy
            InstantiateRedBall();
        }

    }

    private void InstantiateRedBall()
    {
        Instantiate(redball);
    }
}
