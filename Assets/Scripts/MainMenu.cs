using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public GameObject loadingScreen;
    public GameObject mainMenu;
    public GameObject howToPlayUI;
    public Slider slider;
    public TextMeshProUGUI progressPercentage;

    public void PlayGame() {
        StartCoroutine(LoadAsyncronously(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadAsyncronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        mainMenu.SetActive(false);
        loadingScreen.SetActive(true); 

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            progressPercentage.text = (progress * 100f).ToString("0") + "%";

            yield return null;
        }

    }

    public void QuitGame() {
        Application.Quit();
    }

    public void Back()
    {
        howToPlayUI.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void HowToPlay()
    {
        mainMenu.SetActive(false);
        howToPlayUI.SetActive(true);
    }
}
