using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneOnLevelComplete : MonoBehaviour
{
    public string nextScene;
    public float delay = 0f;


    public void LoadScene()
    {
        Invoke("LoadSceneInternal", delay);
    }

    private void LoadSceneInternal()
    {
        ScenesManager.FadeAndLoadScene(nextScene);
    }

}
