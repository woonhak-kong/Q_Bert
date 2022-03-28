using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneUIController : MonoBehaviour
{

    public GameObject PauseUI;

    public List<GameObject> Lifes;

    public GameObject GameOverText;

    public ScoreUI ScoreUI;

    public GameObject ChangeToBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseUI.activeInHierarchy)
            {
                SetPauseMenu(true);
            }
            else
            {
                SetPauseMenu(false);
            }
        }
    }

    public void OnClickResume()
    {
        SetPauseMenu(false);
    }

    public void OnClickToMain()
    {
        SetPauseMenu(false);
        GameManager.Instance().GameStop();
        GameManager.Instance().StopAllCoroutines();
        SceneManager.LoadScene("StartScene");
    }

    private void SetPauseMenu(bool active)
    {
        PauseUI.SetActive(active);
        Time.timeScale = active ? 0.0f : 1.0f;
    }

    public void ShowHowManyLifes(int num)
    {
        if (Lifes.Count < num)
        {
            Debug.Log("Error - out of range");
            return;
        }
        foreach (var obj in Lifes)
        {
            obj.SetActive(false);
        }

        for (int i = 0; i < num; i++)
        {
            Lifes[i].SetActive(true);
        }
    }

    public void ShowGameOver()
    {
        GameOverText.SetActive(true);
        StartCoroutine(GotoMainAfterFewMin());
    }

    private IEnumerator GotoMainAfterFewMin()
    {
        yield return new WaitForSeconds(3.0f);
        OnClickToMain();
    }

    public void AddScore(int value)
    {
        ScoreUI.AddScore(value);
    }

    

}
