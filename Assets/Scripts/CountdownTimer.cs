using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float currentTime;
    public float startingTime;
    bool stop;
    public TextMeshProUGUI countdownText;

    void Start() {
        currentTime = startingTime;
        stop = false;
    }

    void Update() {
        if(!stop) {
            currentTime -= 1 * Time.deltaTime;
            if(currentTime <= 5.45)
            {
                countdownText.color = Color.red;
                if(currentTime <= 0.45)
                {
                    currentTime = 0;
                    FindObjectOfType<GameManager>().GameOver();
                }
            }
            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);
            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        }
    }

    public void StopTimer() {
        stop = true;
    }
}
