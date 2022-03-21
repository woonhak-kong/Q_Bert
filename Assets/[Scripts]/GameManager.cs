using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject redball;
    public GameObject greenBall;
    public GameObject snake;
    public int NumOfSnake { get; set; }
    public bool IsCoilyDead { get; set; } = false;
    public bool IsPlayerDead { get; set; } = false;

    [SerializeField]
    private Blocks _blocksScript;

    private GameObject _playerPosition;
    private GameObject _leftSpinpad;
    private GameObject _rightSpinpad;

    private static GameManager instance = null;

    private bool _isPlayingGame = false;

    private List<Observer> _observers = new List<Observer>();

    private PlaySceneUIController _uiController;

    private int _leftLife;

    
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
            _leftLife = 2;
            //_enemySpawnCoroutine = EnemySpawn();
            StartCoroutine(EnemySpawn());
            _playerPosition = GameObject.Find("PlayerPosition");
            _leftSpinpad = GameObject.Find("LeftPadOrigin");
            _rightSpinpad = GameObject.Find("RightPadOrigin");
            _uiController = GameObject.FindObjectOfType<PlaySceneUIController>();
            _uiController.ShowHowManyLifes(_leftLife);
            RemoveAllObservers();
            SetDistanceBetweenSpinpadAndBlocks();
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
            if (IsCoilyDead)
            {
                yield return new WaitForSeconds(5.0f);
                IsCoilyDead = false;
            }
            if (IsPlayerDead)
            {
                yield return new WaitForSeconds(2.0f);
                IsPlayerDead = false;
            }
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

    public void SetDistanceBetweenSpinpadAndBlocks()
    {
        GameObject[] blocks = GetBlocksScript().GetBlocks();
        foreach (GameObject obj in blocks)
        {
            if (obj != null)
            {
                float distance = Vector2.Distance(obj.transform.position, _leftSpinpad.transform.position);
                obj.GetComponent<Block>().DistanceFromLeftSpinPad = distance;
                distance = Vector2.Distance(obj.transform.position, _rightSpinpad.transform.position);
                obj.GetComponent<Block>().DistanceFromRightSpinPad = distance;
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

    public void NotifyObserversDie()
    {
        foreach (Observer ob in _observers)
        {
            ob.Notify("die");
        }
    }

    public void PlayerDead()
    {
        _leftLife--;
        _uiController.ShowHowManyLifes(_leftLife);
        if (_leftLife < 0)
        {
            // game over
            _uiController.ShowGameOver();
        }
        IsPlayerDead = true;
    }

    public int GetLife()
    {
        return _leftLife;
    }
}
