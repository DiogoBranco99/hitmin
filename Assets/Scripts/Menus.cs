using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menus : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject levelCompleteUI;
    public GameObject crosshairUI;
    public GameObject Player;
    public GameObject WeaponHolder;
    public GameObject timeLeft;
    public GameObject seconds;
    public GameObject hotOrCold;
    public GameObject healthBar;
    public GameObject ammoDisplay;
    public GameObject clue;
    public GameObject extraHealth;
    public GameObject extraAmmo;
    public GameObject[] externalSounds;
    public GameObject howToPlayUI;
    private bool canPause;
    private bool canResume;
    private bool hotOrColdActive;

    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressPercentage;

    void Start() {
        pauseMenuUI.SetActive(false);
        canPause = true;
        canResume = false;
        externalSounds = GameObject.FindGameObjectsWithTag("ExternalSounds");
    }
 
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                if(canResume)
                    Resume();
            }
            else {
                if(canPause)
                    Pause();
            }
        }
    }

    public void Resume() {
        canPause = true;
        canResume = false;
        if(hotOrColdActive)
        {
            hotOrCold.SetActive(true);
            hotOrColdActive = false;
        }
        pauseMenuUI.SetActive(false);
        crosshairUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        FindObjectOfType<EnemyAI>().setgameIsPaused(false);
        FindObjectOfType<MouseLook>().setPaused(false);
        GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
        for (int i = 0; i < minions.Length; i++)
        {
            minions[i].GetComponent<MinionAI>().setgameIsPaused(false);
        }
        Player.GetComponent<PlayerMovement>().enabled = true;
        WeaponHolder.transform.GetChild(0).GetComponent<Gun>().enabled = true;
        WeaponHolder.transform.GetChild(1).GetComponent<Gun>().enabled = true;
        for (int i = 0; i < externalSounds.Length; i++)
        {
            externalSounds[i].GetComponent<AudioSource>().Play();
        }
        Cursor.visible = false;
    }
 
    void Pause() {
        canPause = false;
        canResume = true;
        if(hotOrCold.active)
        {
            hotOrColdActive = true;
        }
        crosshairUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        FindObjectOfType<EnemyAI>().setgameIsPaused(true);
        FindObjectOfType<MouseLook>().setPaused(true);
        GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
        for (int i = 0; i < minions.Length; i++)
        {
            minions[i].GetComponent<MinionAI>().setgameIsPaused(true);
        }
        WeaponHolder.transform.GetChild(0).GetComponent<Gun>().enabled = false;
        WeaponHolder.transform.GetChild(1).GetComponent<Gun>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
        for (int i = 0; i < externalSounds.Length; i++)
        {
           externalSounds[i].GetComponent<AudioSource>().Stop();   
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void GameOver()
    {
        canPause = false;
        canResume = false;
        crosshairUI.SetActive(false);
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        FindObjectOfType<EnemyAI>().setgameIsPaused(true);
        FindObjectOfType<MouseLook>().setPaused(true);
        GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
        for (int i = 0; i < minions.Length; i++)
        {
            minions[i].GetComponent<MinionAI>().setgameIsPaused(true);
        }
        WeaponHolder.transform.GetChild(0).GetComponent<Gun>().enabled = false;
        WeaponHolder.transform.GetChild(1).GetComponent<Gun>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
        for (int i = 0; i < externalSounds.Length; i++)
        {
            externalSounds[i].GetComponent<AudioSource>().Stop();
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void LevelComplete()
    {
        canPause = false;
        canResume = false;
        crosshairUI.SetActive(false);
        levelCompleteUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        FindObjectOfType<EnemyAI>().setgameIsPaused(true);
        FindObjectOfType<MouseLook>().setPaused(true);
        GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
        for (int i = 0; i < minions.Length; i++)
        {
            minions[i].GetComponent<MinionAI>().setgameIsPaused(true);
        }
        WeaponHolder.transform.GetChild(0).GetComponent<Gun>().enabled = false;
        WeaponHolder.transform.GetChild(1).GetComponent<Gun>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
        for (int i = 0; i < externalSounds.Length; i++)
        {
            externalSounds[i].GetComponent<AudioSource>().Stop();
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void Restart()
    {
        StartCoroutine(LoadAsyncronously(SceneManager.GetActiveScene().buildIndex));
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    IEnumerator LoadAsyncronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        timeLeft.SetActive(false);
        seconds.SetActive(false);
        clue.SetActive(false);
        extraHealth.SetActive(false);
        extraAmmo.SetActive(false);
        ammoDisplay.SetActive(false);
        healthBar.SetActive(false);
        hotOrCold.SetActive(false);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            progressPercentage.text = (progress * 100f).ToString("0") + "%";

            yield return null;
        }
    }

    public void LoadMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void Back()
    {
        howToPlayUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        timeLeft.SetActive(true);
        seconds.SetActive(true);
        clue.SetActive(true);
        extraHealth.SetActive(true);
        extraAmmo.SetActive(true);
        ammoDisplay.SetActive(true);
        healthBar.SetActive(true);
        if (hotOrColdActive)
        {
            hotOrCold.SetActive(true);
        }
        canResume = true;
    }

    public void HowToPlay()
    {
        pauseMenuUI.SetActive(false);
        howToPlayUI.SetActive(true);
        timeLeft.SetActive(false);
        seconds.SetActive(false);
        clue.SetActive(false);
        extraHealth.SetActive(false);
        extraAmmo.SetActive(false);
        ammoDisplay.SetActive(false);
        healthBar.SetActive(false);
        hotOrCold.SetActive(false);
        canResume = false;
    }

}
