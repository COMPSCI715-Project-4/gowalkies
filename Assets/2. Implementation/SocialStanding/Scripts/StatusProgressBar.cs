using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIWidgets;
using PedometerU.Tests;

public class StatusProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    public float fillSpeed = 0.5f;
    public Text statusText;
    public static int currentStatus;
    public float timer = 0.01f;
    public float max;
    private bool fill = true;
    private bool paused = true;
    private float initialMax;
    // Step average variables
    private int steps = 0;
    public float stepAverageGoal = 28 / 20;
    public int timeframe = 20;
    private bool increase = false;
    private bool decrease = false;
    private float stepAverage;
    private StepCounter stepCounter;

    List<string> statusNames = new List<string> { "Pet Newbie", "Pet Lover", "Pet Master", "Pet Legend" };
    private List<string> unlocked = new List<string> { "Pet Newbie" };
    private bool keepTiming = true;
    public GameObject notifyPad;
    public Text nextStatusText;
    public Text newStatusText;
    public Text oldStatusText;
    public Text congratsText;
    public Dropdown statusesUnlocked;
    public Text keepStatusText;
    public Text lossStatusText;


    private void Start()
    {
        initialMax = max;
        currentStatus = 0;
        statusText.text = statusNames[currentStatus];
        statusesUnlocked.ClearOptions();
        statusesUnlocked.AddOptions(unlocked);
        stepAverage = stepAverageGoal;
        stepCounter = GetComponent<StepCounter>();
        Timer();
    }

    private void Update()
    {
        steps = stepCounter.GetSteps();

        // Testing purposes
        if (Input.GetKeyDown(KeyCode.RightArrow))
            stepAverage = stepAverageGoal;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            stepAverage = 0;

        if (timer < max && keepTiming)
        {
            // We ignore the first timeframe and check if the time is a multiple of the timeframe e.g. 20
            if ((int)timer % timeframe == 0)
            {
                stepAverage = steps / timeframe;
                if (stepAverage >= stepAverageGoal)
                {
                    fill = true;
                    increase = true;
                    decrease = false;
                }
                else if (stepAverage < stepAverageGoal)
                {
                    fill = false;
                    decrease = true;
                    increase = false;
                }
            }
            if (increase)
                progressSlider.value = timer / max;
            else if (decrease)
                progressSlider.value = timer / max;
            Timer();
            if (progressSlider.value == 0 && currentStatus != 0 && timer <= 0)
                ResetTimer();
        }
        else if (currentStatus == 3 && fill != false)
        {
            timer = max - 0.01f;
            paused = true;
        }
        else if (timer >= max)
            ResetTimer();
    }

    public void Timer()
    {
        if (fill == true && !paused)
            timer += Time.deltaTime;
        else if (fill == false && !paused)
            timer -= Time.deltaTime;
        if (timer < 0)
            timer = 0;
    }

    public void PauseTimer()
    {
        paused = true;
        keepStatusText.text = "Keep Status: " + statusNames[currentStatus];
        lossStatusText.text = "Lose Status: " + statusNames[currentStatus + 1];
    }

    public void ResumeTimer()
    {
        paused = false;
    }

    public void ResetTimer()
    {
        timer = 0.01f;
        keepTiming = false;
        notifyPad.SetActive(true);

        GameObject statusPanel = notifyPad.transform.GetChild(2).gameObject;
        GameObject increaseImage = statusPanel.transform.GetChild(0).gameObject;
        GameObject decreaseImage = statusPanel.transform.GetChild(1).gameObject;

        if (fill == true && currentStatus < 3)
        {
            progressSlider.value = 0f;
            increaseImage.SetActive(true);
            decreaseImage.SetActive(false);
            newStatusText.text = statusNames[currentStatus + 1];
            oldStatusText.text = statusNames[currentStatus];
            congratsText.text = "Congratulations! You have increased your status to " + statusNames[currentStatus + 1] + "!";
            if (currentStatus >= 2)
            {
                nextStatusText.text = "Congrats you've reached the highest social standing!";
            }
            else
                nextStatusText.text = "Next Status: " + statusNames[currentStatus + 2];
            
        }
        else if (fill == false)
        {
            progressSlider.value = max;
            increaseImage.SetActive(false);
            decreaseImage.SetActive(true);
            newStatusText.text = statusNames[1];
            if (currentStatus > 0)
            {
                oldStatusText.text = statusNames[0];
                congratsText.text = "Oh no! You lost all your social standing!";
            }
            nextStatusText.text = "Next Status: " + statusNames[1];
        }
    }

    public void closePanel()
    {
        keepTiming = true;
        notifyPad.SetActive(false);

        if (fill == true)
        {
            max *= 2;
            if (currentStatus < 4)
                currentStatus++;
            statusText.text = statusNames[currentStatus];

            // Add the new status as an option for the dropdown menu
            unlocked.Add(statusNames[currentStatus]);
            statusesUnlocked.ClearOptions();
            statusesUnlocked.AddOptions(unlocked);
            statusesUnlocked.captionText.text = statusNames[currentStatus];
        }
        else if (fill == false)
        {
            max = initialMax;
            // Reset all statuses as options for the dropdown menu
            statusesUnlocked.ClearOptions();
            List<string> m_DropOptions = new List<string> { statusNames[0] };
            statusesUnlocked.AddOptions(m_DropOptions);
            statusesUnlocked.captionText.text = statusNames[currentStatus];

            if (currentStatus > 0)
                currentStatus = 0;
            statusText.text = statusNames[currentStatus];
        }
        fill = true;
        stepAverage = stepAverageGoal;
    }
}