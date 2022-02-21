using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
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
        SoundManager.Instance.PlaySound(Sounds.StartSound);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    
}
