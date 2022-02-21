using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneUIController : MonoBehaviour
{

    public GameObject PauseUI;

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

}
