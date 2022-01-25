using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public List<Sprite> Numbers;

    private static Utils instance = null;

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

    public static Utils Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void SetNumberSprite(int value, List<GameObject> list)
    {
        if (value > 99999)
        {
            Debug.LogError("Value is greater than 99999");
            return;
        }

        int total = 5;
        int offset = 0;
        int restNum = 0;
        int[] position = { 0, 0, 0, 0, 0 };

        position[0] = value / 10000;
        restNum = value % 10000;

        position[1] = restNum / 1000;
        restNum = restNum % 1000;

        position[2] = restNum / 100;
        restNum = restNum % 100;

        position[3] = restNum / 10;
        restNum = restNum % 10;

        position[4] = restNum;

        if (position[0] == 0)
        {
            total--;
            offset++;
            if (position[1] == 0)
            {
                total--;
                offset++;
                if (position[2] == 0)
                {
                    total--;
                    offset++;
                    if (position[3] == 0)
                    {
                        total--;
                        offset++;
                    }
                }
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<SpriteRenderer>().enabled = false;
        }

        for (int i = 0; i < total; i++)
        {
            SpriteRenderer tmp = list[i].GetComponent<SpriteRenderer>();
            tmp.enabled = true;
            tmp.sprite = Numbers[position[i + offset]];
        }
    }
}
