using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject redball;
    public GameObject greenBall;
    public GameObject snake;

    [SerializeField]
    private Blocks _blocksScript;

    private static GameManager instance = null;

    private bool _isPlayingGame = false;

    public int NumOfSnake { get; set; }


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
        NumOfSnake = 0;
        //_enemySpawnCoroutine = EnemySpawn();
        StartCoroutine(EnemySpawn());
    }

    public Blocks GetBlocksScript()
    {
        return _blocksScript;
    }

    private IEnumerator EnemySpawn()
    {
        int snakeReady = 0;

        //yield return new WaitForSeconds(3.0f);
        while (_isPlayingGame)
        {
            yield return new WaitForSeconds(2.2f);

            if (NumOfSnake <= 0 && snakeReady >= 3) // if there is no snake
            {
                //make snake
                InstantiateSnake();
                snakeReady = 0;
            }
            else
            {
                float coefficient = Random.Range(0.0f, 1.0f);

                //Debug.Log(coefficient);
                if (coefficient > 0.20f)
                {
                    InstantiateRedBall();
                }
                else
                {
                    InstantiateGreenBall();
                }
                snakeReady++;
            }
         
        }

    }

    private void InstantiateRedBall()
    {
        Instantiate(redball);
    }

    private void InstantiateGreenBall()
    {
        Instantiate(greenBall);
    }

    private void InstantiateSnake()
    {
        Instantiate(snake);
    }



    public void GameComplete()
    {
        _isPlayingGame = false;
    }
}
