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
    private string musicStr = "music_2";

    public bool godMode = true;
    public bool muteMusic = false;
    public bool muteFx = false;

    public GameEvent onPlayerDiedEvent;
    public BoolVariable allowPlayerInput;
    public bool playOnPlayerDiedEvent;

    public static bool loadingScene = false;

    public Button leftInput;
    public Button rightInput;
    public Button upInput;


    public void SetNotLoadingSceneToFalseAfterTime(float time)
    {
        Invoke("SetFalseLoadingScene", time);
    }

    private void SetFalseLoadingScene()
    {
        loadingScene = false;
    }


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

        PlayMusic();
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

    public void DisableInputButton(string inputName)
    {
        switch(inputName)
        {
            case "left":
                leftInput.GetComponent<InputButtonScript>().DisableButton();
            break;
            case "right":
                rightInput.GetComponent<InputButtonScript>().DisableButton();
            break;
            case "up":
                upInput.GetComponent<InputButtonScript>().DisableButton();
            break;
        }

    }

    public void EnableInputButton(string inputName)
    {
        switch(inputName)
        {
            case "left":
                leftInput.GetComponent<InputButtonScript>().EnableButton();
            break;
            case "right":
                rightInput.GetComponent<InputButtonScript>().EnableButton();
            break;
            case "up":
                upInput.GetComponent<InputButtonScript>().EnableButton();
            break;
        }

    }


    void InitGame()
    {
        PlayMusic();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (allowPlayerInput && Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.Return))
        {
            Restart();
        }
        else if (playOnPlayerDiedEvent)
        {
            Restart();
            playOnPlayerDiedEvent = false;
        }
    }

    public void GameOver()
    {
        Restart();
    }

    public void Restart()
    {
        if (loadingScene) return;
        allowPlayerInput.SetValue(false);
        onPlayerDiedEvent?.Raise();
    }

    private string PickSound(string[] soundsStr)
    {
        int index = Random.Range(0, soundsStr.Length);
        return soundsStr[index];
    }

    public SMSound PlaySound(string[] soundsStr)
    {
        if (muteFx)
        {
            return null;
        }
        return SoundManager.PlaySound(PickSound(soundsStr));
    }

    public SMSound PlaySound(string soundsStr)
    {
        if (muteFx)
        {
            return null;
        }
        return SoundManager.PlaySound(soundsStr);
    }

    public void PlayMusic()
    {
        if (muteMusic)
        {
            return;
        }
        SoundManager.PlayMusic(musicStr);
    }

    public SMSound PlaySoundUI(string soundsStr)
    {
        if (muteFx)
        {
            return null;
        }
        return SoundManager.PlaySoundUI(soundsStr);
    }

    public void StopSound(SMSound smsound)
    {
        SoundManager.StopSound(smsound);
    }


}

