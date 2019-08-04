using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

  // Use this for initialization
  void Start () {
    Invoke("NextScene", 10f);
  }

  // Update is called once per frame
  void Update () {
     if(Input.GetButtonDown("Submit")) {
       NextScene();
     }
  }

  void NextScene() {
    SceneManager.LoadScene("Credits");
  }
}
