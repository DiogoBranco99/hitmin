using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject crosshairUI;
    public GameObject Player;
    public GameObject WeaponHolder;

    void Start() {
        pauseMenuUI.SetActive(false);
    }
 
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {
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

    public void LoadMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void QuitGame() {
        Application.Quit();
    }

}
