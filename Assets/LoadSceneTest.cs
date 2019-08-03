using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneTest : MonoBehaviour
{
    public string sceneName = "";

    void Start()
    {
        var button = GetComponent<Button>();
        if (button) button.onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        ScenesManager.FadeAndLoadScene(sceneName);
    }
}
