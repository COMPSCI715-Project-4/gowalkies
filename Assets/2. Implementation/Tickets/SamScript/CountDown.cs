using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Game counter
public class CountDown : MonoBehaviour
{   
    // Variables and constants
    float currentTime = 0f;
    float startingTime = 10f;
    public static bool check = false;
    public Text countdownText;

    // Start is called before the first frame update
    void Start()
    {   
        // Initialize the counter
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {   
        check = false;
        // Count down the timer
        currentTime -= 1 * Time.deltaTime;
        // Display the timer
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
