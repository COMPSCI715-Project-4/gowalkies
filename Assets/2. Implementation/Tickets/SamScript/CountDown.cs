using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 10f;
    public static bool check = false;
    public Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {   
        check = false;
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");
        if (currentTime < 0)
        {   
            // Reset the Timer
            // currentTime = startingTime;        
            countdownText.text = "Expired";
            check = true;   
        }
    }
}
