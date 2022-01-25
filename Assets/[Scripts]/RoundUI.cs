using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundUI : MonoBehaviour
{
    public List<GameObject> gameObjects;
    private int _round = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetRound(_round);
    }

    private void SetRound(int value)
    {
        Utils.Instance.SetNumberSprite(value, gameObjects);
    }

    public void AddLevel(int value)
    {
        _round += value;
        SetRound(_round);
    }

}
