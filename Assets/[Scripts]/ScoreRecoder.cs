using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecoder : MonoBehaviour
{
    public static ScoreRecoder Instance { get; set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<int> GetScoreByList()
    {
        List<int> list = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey(i.ToString()))
            {
                list.Add(PlayerPrefs.GetInt(i.ToString()));
            }
        }
       
        return list;
    }

    public void SaveScore(int value)
    {
        if (value <= 0)
            return;

        List<int> list = new List<int>();

        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey(i.ToString()))
            {
                list.Add(PlayerPrefs.GetInt(i.ToString()));
            }
        }

        list.Add(value);
        list.Sort(Comparer<int>.Create( (x, y) => x > y ? -1 : 1) );

        if (list.Count > 10)
        {
            list.RemoveAt(list.Count - 1);
        }

        for (int i = 0; i < list.Count; i++)
        {
            print(list[i]);
        }

        PlayerPrefs.DeleteAll();
        for (int i = 0; i < list.Count; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), list[i]);
        }

        PlayerPrefs.Save();
    }
}
