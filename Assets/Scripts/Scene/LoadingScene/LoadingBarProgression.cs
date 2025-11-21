using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBarProgression : MonoBehaviour
{
    [SerializeField] private Slider loadingBar;

    [SerializeField] private float waitTime = 45.0f;
    private float progress = 0f;

    private const string GAMEPLAY_SCENE_NAME = "GameplayScene";

    private void Update()
    {
        if (progress < waitTime)
        {
            progress += Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(GAMEPLAY_SCENE_NAME);
        }

        SetLoadingProgress(progress);
    }

    public void SetLoadingProgress(float progress)
    {
        loadingBar.value = progress;
    }
}
