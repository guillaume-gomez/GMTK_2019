using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneOnLevelComplete : MonoBehaviour
{
    public string nextScene;
    public float delay = 0.5f;


    public void LoadNextScene()
    {
        Invoke("LoadNextSceneInternal", delay);
    }

    private void LoadNextSceneInternal()
    {
        ScenesManager.FadeAndLoadScene(nextScene);
    }

}
