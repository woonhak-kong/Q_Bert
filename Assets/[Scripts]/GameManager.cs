using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject redball;
    public GameObject greenBall;
    public GameObject snake;

    [SerializeField]
    private Blocks _blocksScript;

    private GameObject _playerPosition;

    private static GameManager instance = null;

    private bool _isPlayingGame = false;

    private List<Observer> _observers = new List<Observer>();

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
        
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "PlayScene")
        {
            _blocksScript = GameObject.Find("Blocks").gameObject.GetComponent<Blocks>();
            _isPlayingGame = true;
            NumOfSnake = 0;
            //_enemySpawnCoroutine = EnemySpawn();
            StartCoroutine(EnemySpawn());
            _playerPosition = GameObject.Find("PlayerPosition");
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
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

    public void GameStop()
    {
        _isPlayingGame = false;
    }

    public void SetDistanceBetweenPlayerAndBlocks()
    {
        GameObject[] blocks = GetBlocksScript().GetBlocks();
        foreach (GameObject obj in blocks)
        {
            if (obj != null)
            {
                float distance = Vector2.Distance(obj.transform.position, _playerPosition.transform.position);
                obj.GetComponent<Block>().DistanceFromPlayer = distance;
            }
        }
    }

    public void AddObserver(Observer ob)
    {
        _observers.Add(ob);
    }

    public void RemoveObserver(Observer ob)
    {
        _observers.Remove(ob);
    }

    public void RemoveAllObservers()
    {
        _observers.Clear();
        Debug.Log("Observer Count" + _observers.Count);
    }

    public void NotifyObservers()
    {
        foreach (Observer ob in _observers)
        {
            ob.Notify();
        }
    }
}
