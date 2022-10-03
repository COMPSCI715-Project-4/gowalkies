using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class IntensityTImer : MonoBehaviour
{
    private float timeDuration = 1f * 60f;
    private float timer;
    private float step;

    [SerializeField]
    private Text firstMinute;
    [SerializeField]
    private Text secondMinute;
    [SerializeField]
    private Text firstSecond;
    [SerializeField]
    private Text secondSecond;
    [SerializeField]
    private Text seperator;

    [SerializeField]
    private bool countDown;

    //[SerializeField]
    //private Text steps;

    //[SerializeField]
    //private GameObject NotificationPanel;

    private float flashTimer;
    private float flashDuration = 1f;
    public int averageSteps;
    private int previousAmount = 0;
 

    // Update is called once per frame
    void Update()
    {
        if (countDown && timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay(timer);
        }
        else if (!countDown && timer < timeDuration)
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay(timer);

        }
        else 
        {
            Flash();
        }
    }


    public void startTimer()
    {
        ResetTimer();
    }

    private void ResetTimer()
    {
        if (countDown)
        {

            timer = timeDuration;

        }
        else
        {
            timer = 0;
        }

        SetTextDisplay(true);


    }

    private void UpdateTimerDisplay( float time)
    {
        if (time < 0)
        {
            timer = 0;
        }

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        //if (seconds % 20 == 0)
        //{
        //    if (minutes != 0)
        //    {
        //        int currentStep = int.Parse(steps.text);
        //        int actualStepTake = currentStep - previousAmount;
        //        previousAmount = currentStep;
        //        if (actualStepTake < averageSteps)
        //        {
        //            NotificationPanel.SetActive(true);
                    
        //        }
        //    }
        //}

        string currrentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        firstMinute.text = currrentTime[0].ToString();
        secondMinute.text = currrentTime[1].ToString();
        firstSecond.text = currrentTime[2].ToString();
        secondSecond.text = currrentTime[3].ToString();
        //steps.text = "60";
        


    }

    private void Flash()
    {
        if (countDown && timer != 0)
        {
            timer = 0;
            UpdateTimerDisplay(timer);
        }

        if (!countDown && timer != timeDuration)
        {
            timer = timeDuration;
            UpdateTimerDisplay(timer);
        }

        if(flashTimer <= 0)
        {
            flashTimer = flashDuration;
        }
        else if (flashTimer >=  flashDuration / 2)
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(false); 
        }
        else
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(true);
        }

    }

    private void SetTextDisplay(bool enabled)
    {
        firstMinute.enabled = enabled;
        secondMinute.enabled = enabled;
        firstSecond.enabled = enabled;
        secondSecond.enabled = enabled;
        seperator.enabled = enabled; 
    }
}
