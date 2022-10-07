using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UIWidgets;
using PedometerU.Tests;

public class EvolutionProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    public float fillSpeed = 0.5f;
    public static int currentEvo;
    [SerializeField]
    private float timer = 0.01f;

    public float max;
    private bool fill = true;
    private bool paused = true;
    private float initialMax = 10;
    // Step average variables
    private int steps = 0;
    public int stepAverageGoal;
    private int timeframe = 20;
    private bool increase = false;
    private bool decrease = false;
    private float stepAverage;
    private bool keepTiming = true;
    public GameObject notifyPad;
    public Text nextStatusText;
    public Text newStatusText;
    public Text oldStatusText;
    public Text congratsText;
    public Text evolutionLevelText;
    public Text keepStatusText;
    public Text lossStatusText;
    private Timer t = new Timer();
    
    private GameObject pet;

    private int currentStep;

    private int previousStep = 0;
    private bool gameStart = false;

    [SerializeField]
    private Text currentStepText;
    [SerializeField]
    private Text averageStepText;

    public void startGame()
    {
        //set the personal step goal
        //come from the Intensity Test
        //stepAverageGoal = testAverageStep.averageStep; //this should be uncommented when we exported the app. Link to the average eintensity test

        //this can be accessed and changed from the inspector
        //testing purpose
        stepAverageGoal = testAverageStep.averageStep;
        averageStepText.text = stepAverageGoal.ToString(); 

        initialMax = max;
        currentEvo = 1;
        pet = GameObject.FindWithTag("pets");
        stepAverage = stepAverageGoal;

        //following codes are the timer for calculating the current step
        //handle update function will call every 20 seconds
        Timer();
        gameStart = true;
        t.Elapsed += new ElapsedEventHandler(handleUpdate);
        t.Interval = 20000;
        t.Start();

    }


    //calculate the average step
    private void handleUpdate(object source, ElapsedEventArgs e)
    {

        //testing purpose
        //step increment one by one sec
        //currentStep = TImer.TimerCurrentStep;


        currentStep = StepCounter.currentSteps; //should be uncommented when we want to exported the app

        stepAverage = currentStep - previousStep;
        currentStepText.text = stepAverage.ToString();
        previousStep = currentStep;
        Debug.Log(stepAverage); 
    }

    private void Update()
    {

        if (gameStart)
        {

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
                if (progressSlider.value == 0 && currentEvo != 1 && timer <= 0)
                    ResetTimer();
            }
            else if (currentEvo == 4 && fill != false)
            {
                timer = max - 0.01f;
                paused = true;
            }
            else if (timer >= max)
            {
                //Debug.Log("Here1");
                ResetTimer();
                ChangeSize();
            }


        }


    }

    public void ChangeSize()
    {
        if (pet != null)
        {
            Vector3 size = new Vector3(0.2f, 0.2f, 0.2f);
            if (fill == true)
                pet.transform.localScale = (currentEvo + 1) * size;
            else if (fill == false)
                pet.transform.localScale = 1 * size;
            Timer();
        }
        else
            pet = GameObject.FindWithTag("pets");
    }

    public void Timer()
    {
        if (fill == true && !paused)
            timer += Time.deltaTime;
        else if (fill == false && !paused)
            timer -= Time.deltaTime;
        if (timer < 0)
            timer = 0f;
    }

    public void PauseTimer()
    {
        paused = true;
        keepStatusText.text = "Keep Level: " + currentEvo;
        lossStatusText.text = "Lose Level: " + (currentEvo + 1);
    }

    public void ResumeTimer()
    {
        paused = false;
    }

    public void ResetTimer()
    {
        //Debug.Log("Here2");
        timer = 0.01f;
        keepTiming = false;
        notifyPad.SetActive(true);

        GameObject statusPanel = notifyPad.transform.GetChild(2).gameObject;
        GameObject increaseImage = statusPanel.transform.GetChild(0).gameObject;
        GameObject decreaseImage = statusPanel.transform.GetChild(1).gameObject;

        if (fill == true)
        {
            progressSlider.value = 0f;
            increaseImage.SetActive(true);
            decreaseImage.SetActive(false);
            newStatusText.text = "Level " + (currentEvo + 1);
            oldStatusText.text = "Level " + currentEvo;
            congratsText.text = "Congratulations! Your pet has evolved to level " + (currentEvo + 1) + "!";
            if (currentEvo >= 2)
            {
                nextStatusText.text = "Congrats you've reached max level!";
            }
            else
                nextStatusText.text = "Next Evolution Level: " + (currentEvo + 2);
        }
        else if (fill == false)
        {
            progressSlider.value = max;
            increaseImage.SetActive(false);
            decreaseImage.SetActive(true);
            newStatusText.text = "Level " + currentEvo;
            if (currentEvo > 0)
            {
                oldStatusText.text = "Level " + (currentEvo - 1);
                congratsText.text = "Oh no! Your pet has lost all its levels! Your pet is now level 1!";
            }
            nextStatusText.text = "Next Evolution Level: 2";
        }
    }

    public void ClosePanel()
    {
        keepTiming = true;
        notifyPad.SetActive(false);

        if (fill == true)
        {
            max *= 2;
            if (currentEvo < 4)
                currentEvo++;
        }
        else if (fill == false)
        {
            max = initialMax;
            currentEvo = 1;
        }
        evolutionLevelText.text = "Level " + currentEvo;
    }
}
