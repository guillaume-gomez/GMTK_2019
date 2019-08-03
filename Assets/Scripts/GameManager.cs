using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

  public class GameManager : MonoBehaviour
  {
    public static GameManager instance = null;
    private int level = 0;
    public bool godMode = true;

    //Awake is always called before any Start functions
    void Awake()
    {
      if (instance == null){
        instance = this;
      }
      else if (instance != this) {
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

      if(Input.GetKey("r") || Input.GetKey("return")) {
        Restart();
      }
    }

    public void GameOver()
    {
    }

    private void Restart()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

}

