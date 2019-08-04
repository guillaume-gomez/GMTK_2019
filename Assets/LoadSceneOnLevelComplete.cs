using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneOnLevelComplete : MonoBehaviour
{
    public string nextScene;
    public float loadDelay = 1f;




    public void LoadNextScene()
    {
        Invoke("LoadNextSceneInternal", loadDelay);
    }

    private void LoadNextSceneInternal()
    {
        ScenesManager.FadeAndLoadScene(nextScene);
        GameManager.loadingScene = true;
        GameManager.instance.SetNotLoadingSceneToFalseAfterTime(1.22f);
    }



}
