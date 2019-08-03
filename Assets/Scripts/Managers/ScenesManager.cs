using DG.Tweening;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{

    public static ScenesManager Instance;

    public float fadeDuration = 1f;
    private CanvasGroup faderCanvasGroup;

    private Camera fadeCam;
    //private string currentSceneName = "";
    private bool isFading;
    private bool isLoadingScene;
    public string CurrentSceneName = "ScenesManager";

    public static event Action<string> OnBeforeSceneUnloaded;
    public static event Action<string> OnSceneLoaded;

    private bool m_fadeOut = true;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //get cam component or create a new one
        if (fadeCam == null)
        {
            fadeCam = GetComponentInChildren<Camera>();
        }
        if (fadeCam == null)
        {
            GameObject go = new GameObject("FadeCam");
            go.transform.position = Vector3.zero;
            go.transform.SetParent(this.transform);
            go.AddComponent<Camera>();
            fadeCam = go.GetComponent<Camera>();
            go.SetActive(false);
        }

        if (fadeCam)
        {
            var listener = fadeCam.GetComponent<AudioListener>();
            if (listener) Destroy(listener);
        }

        //get canvas component or create a new one
        if (faderCanvasGroup == null)
        {
            faderCanvasGroup = GetComponentInChildren<CanvasGroup>();
        }
        if (faderCanvasGroup == null)
        {
            GameObject go = new GameObject("FadeCanvas", typeof(Canvas));
            go.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            go.GetComponent<Canvas>().sortingLayerName = "Manager";
            go.GetComponent<Canvas>().sortingOrder = 500;

            faderCanvasGroup = go.AddComponent<CanvasGroup>();
            go.AddComponent<Image>();
            go.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            go.GetComponent<RectTransform>().anchorMax = Vector2.one;
            faderCanvasGroup.alpha = 0f;
            faderCanvasGroup.blocksRaycasts = true;
            DontDestroyOnLoad(go);
            go.SetActive(false);
        }

        ////instantiate and assign loading animation
        //var animGO = Instantiate(loadingAnimationPrefab.gameObject);
        //bool loadAnimFound = false;
        //if (animGO)
        //{
        //    loadingAnimation = animGO.GetComponent<CanvasGroup>();
        //    if (loadingAnimation)
        //    {
        //        loadAnimFound = true;
        //        DontDestroyOnLoad(loadingAnimation);
        //        loadingAnimation.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        Destroy(animGO);
        //    }
        //}

        fadeCam.gameObject.SetActive(false);


    }


    public static void FadeAndLoadScene(string sceneName)
    {
        if (Instance == null)
        {
            GameObject go = new GameObject("ScenesManager");
            Instance = go.AddComponent<ScenesManager>();
        }
        Instance.StartCoroutine(Instance.FadeAndSwitchScenes(sceneName));
    }


    private IEnumerator FadeAndSwitchScenes(string sceneName, bool fadeIn = true, bool fadeOut = true)
    {

        if (isFading || isLoadingScene) yield break;

        isLoadingScene = true;
        m_fadeOut = fadeOut;
        if (fadeIn) { yield return StartCoroutine(FadeCR(1f, fadeDuration)); }

        Scene sceneToUnload = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        OnBeforeSceneUnloaded?.Invoke(sceneToUnload.name);

        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));
        fadeCam.gameObject.SetActive(false);

        if (Camera.main == null) fadeCam.gameObject.SetActive(true);
        Resources.UnloadUnusedAssets();

        yield return SceneManager.UnloadSceneAsync(sceneToUnload);

        OnSceneLoaded?.Invoke(sceneName);

        Instance.CurrentSceneName = sceneName;
        if (fadeOut) { yield return StartCoroutine(FadeCR(0f, fadeDuration)); }

        isLoadingScene = false;
    }

    public IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
        Instance.CurrentSceneName = sceneName;
        System.GC.Collect();
    }

    public IEnumerator FadeCR(float finalAlpha, float fadeDuration = -1)
    {
        if (fadeDuration == -1) fadeDuration = this.fadeDuration;
        isFading = true;
        faderCanvasGroup.gameObject.SetActive(true);
        faderCanvasGroup.blocksRaycasts = true;

        //fade in
        if (finalAlpha >= 0.5f)
        {
            //fade in white
            float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;
            yield return FadeCanvas(faderCanvasGroup, finalAlpha, fadeSpeed);
        }
        //fade out
        else
        {
            float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;
            yield return FadeCanvas(faderCanvasGroup, finalAlpha, fadeSpeed);
            faderCanvasGroup.blocksRaycasts = false;
            faderCanvasGroup.gameObject.SetActive(false);
        }

        if (finalAlpha < 0.1) isFading = false;
        faderCanvasGroup.blocksRaycasts = false;

    }

    private IEnumerator FadeCanvas(CanvasGroup canvas, float finalAlpha, float fadeSpeed)
    {
        float alpha;
        while (!Mathf.Approximately(canvas.alpha, finalAlpha))
        {
            alpha = Mathf.MoveTowards(canvas.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            canvas.alpha = alpha;
            //if (isFading && Mathf.Abs(alpha - finalAlpha) < 0.3f) isFading = false;
            yield return null;
        }
        canvas.alpha = finalAlpha;
    }



}
