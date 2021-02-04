using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotOrCold : MonoBehaviour
{
    public GameObject player;
    public GameObject victim;
    private float previousDistance;
    private float currentDistance;
    public float nearDistance;
    public float intermediateDistance;
    public float farDistance;
    public TextMeshProUGUI hotOrCold;


    void Start () {
        previousDistance = Vector3.Distance(player.transform.position, victim.transform.position);
    }
    
    // Update is called once per frame
    void Update()
    {
        currentDistance = Vector3.Distance(player.transform.position, victim.transform.position);
        if(currentDistance > previousDistance) {
            if(currentDistance > farDistance) {
                hotOrCold.color = Color.blue;
                hotOrCold.text = "Cold";
            }
            else {
                hotOrCold.color = Color.blue;
                hotOrCold.text = "Colder";
            }
            
        }
        if(currentDistance < previousDistance) {
            if(currentDistance <= nearDistance) {
                hotOrCold.color = Color.red;
                hotOrCold.text = "Burning red";
            }
            else if (currentDistance <= intermediateDistance)
            {
                hotOrCold.color = new Color(1f, 0.38f, 0.28f);
                hotOrCold.text = "Very hot";
            }
            else {
                hotOrCold.color = Color.yellow;
                hotOrCold.text = "Hotter";
            }
        }
        previousDistance = currentDistance;
    }
}
