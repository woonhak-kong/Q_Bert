using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
    public GameObject[] ArrowObjects;
    // Start is called before the first frame update

    private ArrowType _currentType;

    private enum ArrowType
    {
        NOTHING,
        ONE,
        TWO,
        COUNT
    }

    void Start()
    {
        _currentType = ArrowType.NOTHING;
        StartCoroutine(ArrowAnimationCo());
    }

    // Update is called once per frame
    void Update()
    {
       
        //print(_currentType);
    }

    IEnumerator ArrowAnimationCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            _currentType++;
            if (_currentType >= ArrowType.COUNT)
            {
                _currentType = ArrowType.NOTHING;
            }
            switch (_currentType)
            {
                case ArrowType.NOTHING:
                    for (int i = 0; i < ArrowObjects.Length; i++)
                    {
                        ArrowObjects[i].SetActive(false);
                    }
                    break;
                case ArrowType.ONE:
                    ArrowObjects[0].SetActive(true);
                    ArrowObjects[1].SetActive(true);
                    ArrowObjects[2].SetActive(false);
                    ArrowObjects[3].SetActive(false);
                    break;
                case ArrowType.TWO:
                    ArrowObjects[0].SetActive(true);
                    ArrowObjects[1].SetActive(true);
                    ArrowObjects[2].SetActive(true);
                    ArrowObjects[3].SetActive(true);
                    break;
            }

        }
    }
}
