using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    [SerializeField]
    //public List<KeyValuePair<int, GameObject>> _blocks = new List<KeyValuePair<int, GameObject>>();
    //public Dictionary<int, GameObject> _blocks = new Dictionary<int, GameObject>();
    private GameObject[] _blocks;
    private Transform[] _transfrom;

    public static int NumOfNonCompleteBlock;
    public static int NumOfSpinBlockLeft;


    private void Awake()
    {
        _transfrom = GetComponentsInChildren<Transform>();
        _blocks = new GameObject[transform.childCount];

        int i = 0;
        foreach (Transform t in _transfrom)
        {
            if (t.gameObject != this.gameObject)
            {
                _blocks[i] = t.gameObject;
                _blocks[i].GetComponent<Block>().Index = i;
                i++;
            }
        }

    }

    private void Start()
    {
        NumOfNonCompleteBlock = 28;
        NumOfSpinBlockLeft = 2;
    }

    public GameObject[] GetBlocks()
    {
        return _blocks;
    }

}
