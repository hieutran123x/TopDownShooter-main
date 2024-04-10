using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneManagerController : MonoBehaviour
{
    public string nextSceneName;
    public float delayBeforeLoading;
    public LoadingSceneController progressBar; 

    private void Start()
    {
        Invoke("CheckProgressAndLoadScene", delayBeforeLoading);
    }

    private void CheckProgressAndLoadScene()
    {
        if (progressBar != null && progressBar.IsProgressBarFull())
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Invoke("CheckProgressAndLoadScene", 0.5f);
        }
    }
}
