using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float currentTime = 0f;
    public float startingTime = 10f;

    public TextMeshProUGUI countdownText;

    void Start() {
        currentTime = startingTime;
    }

    void Update() {
        currentTime -= 1 * Time.deltaTime;
        if(currentTime <= 3.45) {
            countdownText.color = Color.red;
            if(currentTime <= 0) {
                currentTime = 0;
            }
        }
        countdownText.text = currentTime.ToString("00:00");
    }
}
