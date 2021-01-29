using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float currentTime = 0f;
    public float startingTime = 10f;
    bool stop;
    public TextMeshProUGUI countdownText;

    void Start() {
        currentTime = startingTime;
        stop = false;
    }

    void Update() {
        if(!stop) {
            currentTime -= 1 * Time.deltaTime;
            if(currentTime <= 3.45) {
                countdownText.color = Color.red;
                if(currentTime <= 0) {
                    currentTime = 0;
                    NPCHealth npc = FindObjectOfType<NPCHealth>();
                    if(npc.health >= 0f) {
                        FindObjectOfType<GameManager>().LevelComplete();
                    }
                
                }
            }
            countdownText.text = currentTime.ToString("00:00");
        }
    }

    public void StopTimer() {
        stop = true;
    }
}
