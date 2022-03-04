using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuUIController : MonoBehaviour
{
    public GameObject TitleCanvas;
    public GameObject ReaderBoardCanvas;

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
    }

    public void OnClickTitle()
    {
        _state = MenuState.TITLE;
        TitleCanvas.SetActive(true);
        ReaderBoardCanvas.SetActive(false);
    }

    
}
