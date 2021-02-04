using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded;
    public CountdownTimer timer;

    void Start () {
        gameHasEnded = false;
    }

    public void GameOver() {
        if(!gameHasEnded) {
            Debug.Log("Game Over!");
            gameHasEnded = true;
            timer.StopTimer();
            FindObjectOfType<Menus>().GameOver();
        }
    }

    public void LevelComplete() {
        if(!gameHasEnded) {
            Debug.Log("Level Complete!");
            gameHasEnded = true;
            timer.StopTimer();
            FindObjectOfType<Menus>().LevelComplete();
        }
    }

}
