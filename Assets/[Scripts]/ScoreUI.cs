using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class ScoreUI : MonoBehaviour
{

    public List<GameObject> gameObjects;
    private int _mScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetScore(_mScore);
    }

    private void SetScore(int value)
    {
        Utils.Instance.SetNumberSprite(value, gameObjects);
    }

    public void AddScore(int value)
    {
        _mScore += value;
        SetScore(_mScore);
    }

    public int GetScore()
    {
        return _mScore;
    }


}
