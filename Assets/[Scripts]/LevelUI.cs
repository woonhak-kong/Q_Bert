using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public List<GameObject> gameObjects;
    private int _level = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetLevel(_level);
    }
    private void SetLevel(int value)
    {
        Utils.Instance.SetNumberSprite(value, gameObjects);
    }

    public void AddLevel(int value)
    {
        _level += value;
        SetLevel(_level);
    }
}
