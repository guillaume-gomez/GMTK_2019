using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using RoboRyanTron.Unite2017.Events;
using RoboRyanTron.Unite2017.Variables;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private int level = 0;
    public bool godMode = true;
    public GameEvent onPlayerDiedEvent;
    public BoolVariable allowPlayerInput;

    public bool playOnPlayerDiedEvent;

    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }


    //this is called only once, and the parameter tell it to be called only after the scene was loaded
    //(otherwise, our Scene Load callback would be called the very first load, and we don't want that)
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization()
    {
        //register the callback to be called everytime the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //This is called each time a scene is loaded.
    static private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
    }


    void InitGame()
    {
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKey("r") || Input.GetKey("return"))
        {
            bool allow = allowPlayerInput != null && allowPlayerInput.Value;
            if (allow) Restart();
        }
        else if (playOnPlayerDiedEvent)
        {
            onPlayerDiedEvent?.Raise();
            playOnPlayerDiedEvent = false;
        }
    }

    public void GameOver()
    {
    }

    public void Restart()
    {
        onPlayerDiedEvent?.Raise();
        //StartCoroutine(RestartCR());
    }

    private IEnumerator RestartCR()
    {
        //yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        yield return null; //we jump two frames to make sure awake and start functions in new scene have been called 
        yield return null; //actually donno if we need this, just for testing
    }

}

