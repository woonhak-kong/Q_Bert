using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum Direction
    {
        LEFT_UP = 0,
        RIGHT_UP = 1,
        LEFT_DOWN = 2,
        RIGHT_DOWN = 3
    }
    
    public Block[] m_blocks = new Block[4];
    public int Index { set; get; }

    protected bool isComplete = false;

    private float _distanceFromPlayer = 0.0f;
    public float _distanceFromLeftSpinPad = 0.0f;
    public float _distanceFromRightSpinPad = 0.0f;

    public float DistanceFromPlayer
    {
        set => _distanceFromPlayer = value;
        get => _distanceFromPlayer;
    }

    public float DistanceFromLeftSpinPad
    {
        set => _distanceFromLeftSpinPad = value;
        get => _distanceFromLeftSpinPad;
    }

    public float DistanceFromRightSpinPad
    {
        set => _distanceFromRightSpinPad = value;
        get => _distanceFromRightSpinPad;
    }

    public virtual void SetComplete()
    {
        if (!isComplete)
        {
            isComplete = true;
            Sprite[] SpritesData = Resources.LoadAll<Sprite>("Sprites/qbert");
            foreach (Sprite sprite in SpritesData)
            {
                if (sprite.name == "block_yellow_1")
                {
                    GetComponent<SpriteRenderer>().sprite = sprite;
                    GameManager.Instance().UiController.AddScore(25);
                    Blocks.NumOfNonCompleteBlock--;
                    if (Blocks.NumOfNonCompleteBlock == 0)
                    {
                        GameManager.Instance().UiController.AddScore(1000);
                        GameManager.Instance().UiController.AddScore(100 * Blocks.NumOfSpinBlockLeft);
                        GameManager.Instance().GameComplete();
                    }
                    break;
                }
            }
        }

    }
}
