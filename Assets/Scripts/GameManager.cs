using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded;
    public float restartDelay = 5f;
    public CountdownTimer timer;

    void Start () {
        gameHasEnded = false;
    }

    public void GameOver() {
        if(!gameHasEnded) {
            Debug.Log("Game Over!");
            gameHasEnded = true;
            timer.StopTimer();
            // Invoke("Restart", restartDelay);
        }
    }

    void Restart () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelComplete() {
        if(!gameHasEnded) {
            Debug.Log("Level Complete!");
            gameHasEnded = true;
            timer.StopTimer();
            // Invoke("NextLevel", restartDelay);
        }
    }

    void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
