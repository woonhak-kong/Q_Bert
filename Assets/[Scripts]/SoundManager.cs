using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Sounds
{
    public static string StartSound = "Sounds/level-start";
    public static string Hello = "Sounds/hello";
    public static string Speech1 = "Sounds/speech-2";

    public static string QbertJump = "Sounds/qbert_jump";
    public static string JellyJump = "Sounds/JellyJump";
    public static string CoilJump = "Sounds/CoilJump";
}


public class SoundManager : MonoBehaviour
{

    private static SoundManager _instance;

    public Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    private AudioSource _audioSource;


    public static SoundManager Instance
    {
        get
        {
            return _instance;
        } 
    }

    void Awake()
    {
        if (null == _instance)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        _audioSource = GetComponent<AudioSource>();
        AddSounds(Sounds.StartSound);
        AddSounds(Sounds.Hello);
        AddSounds(Sounds.Speech1);
        AddSounds(Sounds.JellyJump);
        AddSounds(Sounds.QbertJump);
        AddSounds(Sounds.CoilJump);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "StartScene")
        {
            _audioSource.PlayOneShot(_audioClips[Sounds.Hello]);

        }
        else if (scene.name == "PlayScene")
        {
            //_audioSource.PlayOneShot(_audioClips[Sounds.Speech1]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        

        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddSounds(string path)
    {
        _audioClips.Add(path, Resources.Load<AudioClip>(path));
    }

    public void PlaySound(string sound)
    {
        _audioSource.PlayOneShot(_audioClips[sound]);
    }
}
