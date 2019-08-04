using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {
  public string nextScene = "Credits";

  // Use this for initialization
  void Start () {
    SoundManager.PlayMusic("music_1");
    //Invoke("NextScene", 10f);
  }

  // Update is called once per frame
  void Update () {
     if(Input.GetButtonDown("Submit")) {
       SoundManager.StopMusic();
       NextScene();
     }
  }

  void NextScene() {
    SceneManager.LoadScene(nextScene);
  }
}
