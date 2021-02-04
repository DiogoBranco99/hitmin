using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject levelCompleteUI;
    public GameObject crosshairUI;
    public GameObject Player;
    public GameObject WeaponHolder;
    private bool canPause;
    private bool canResume;

    void Start() {
        pauseMenuUI.SetActive(false);
        canPause = true;
        canResume = false;
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
        pauseMenuUI.SetActive(false);
        crosshairUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        FindObjectOfType<EnemyAI>().setgameIsPaused(false);
        GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
        for (int i = 0; i < minions.Length; i++)
        {
            minions[i].GetComponent<MinionAI>().setgameIsPaused(false);
        }
        Player.GetComponent<PlayerMovement>().enabled = true;
        WeaponHolder.transform.GetChild(0).GetComponent<Gun>().enabled = true;
        WeaponHolder.transform.GetChild(1).GetComponent<Gun>().enabled = true;
        Cursor.visible = false;
    }
 
    void Pause() {
        canPause = false;
        canResume = true;
        crosshairUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        FindObjectOfType<EnemyAI>().setgameIsPaused(true);
        GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
        for (int i = 0; i < minions.Length; i++)
        {
            minions[i].GetComponent<MinionAI>().setgameIsPaused(true);
        }
        WeaponHolder.transform.GetChild(0).GetComponent<Gun>().enabled = false;
        WeaponHolder.transform.GetChild(1).GetComponent<Gun>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
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
        GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
        for (int i = 0; i < minions.Length; i++)
        {
            minions[i].GetComponent<MinionAI>().setgameIsPaused(true);
        }
        WeaponHolder.transform.GetChild(0).GetComponent<Gun>().enabled = false;
        WeaponHolder.transform.GetChild(1).GetComponent<Gun>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
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
        GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
        for (int i = 0; i < minions.Length; i++)
        {
            minions[i].GetComponent<MinionAI>().setgameIsPaused(true);
        }
        WeaponHolder.transform.GetChild(0).GetComponent<Gun>().enabled = false;
        WeaponHolder.transform.GetChild(1).GetComponent<Gun>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void LoadMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void QuitGame() {
        Application.Quit();
    }

}
