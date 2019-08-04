using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string nextScene = "Credits";
    public BrokenMirror brokenMirror;

    // Use this for initialization
    void Start()
    {
        SoundManager.PlayMusic("music_1");
        //Invoke("NextScene", 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SoundManager.StopMusic();
            StartCoroutine(NextScene());
        }
    }

    IEnumerator NextScene()
    {
        if (brokenMirror)
        {
            brokenMirror.Animate();
            yield return new WaitForSeconds(0.5f);
        }

        ScenesManager.FadeAndLoadScene(nextScene);
    }
}
