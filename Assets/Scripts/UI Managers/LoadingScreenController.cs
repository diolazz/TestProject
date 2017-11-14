using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class is responsible for display loading screen
/// </summary>
public class LoadingScreenController : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private Text progressText;

    void Start()
    {
        LoadScene();
    }

    public void LoadScene(int sceneIndex = 1)
    {
        StartCoroutine(LoadingScene(sceneIndex));
    }

    /// <summary>
    /// Loading scene
    /// </summary>
    /// <param name="sceneIndex"></param>
    /// <returns></returns>
    IEnumerator LoadingScene(int sceneIndex)
    {
        yield return new WaitForSeconds(1);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/.9f);

            loadingBar.value = progress;
            progressText.text = Mathf.RoundToInt(progress*100f) + "%";

            yield return null;
        }

    }
}
