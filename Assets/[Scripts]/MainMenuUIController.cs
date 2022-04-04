using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuUIController : MonoBehaviour
{
    public GameObject TitleCanvas;
    public GameObject ReaderBoardCanvas;
    public Text ScoreListText;

    enum MenuState
    {
        TITLE,
        READER_BOARD
    }
    
    private MenuState _state = MenuState.TITLE;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickReaderBoard()
    {
        _state = MenuState.READER_BOARD;
        TitleCanvas.SetActive(false);
        ReaderBoardCanvas.SetActive(true);

        ScoreListText.text = "";
        List<int> list = ScoreRecoder.Instance.GetScoreByList();
        for (int i = 0; i < list.Count; i++)
        {
            ScoreListText.text += (i+1).ToString() + "   -    " + list[i].ToString() + "\n";
        }
    }

    public void OnClickTitle()
    {
        _state = MenuState.TITLE;
        TitleCanvas.SetActive(true);
        ReaderBoardCanvas.SetActive(false);
    }

    
}
