using System.Collections;
using System.Collections.Generic;
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
    private int stepAverageGoal = 28;
    private int timeframe = 20;
    private bool increase = false;
    private bool decrease = false;
    private float stepAverage;
    private StepCounter stepCounter;

    private bool keepTiming = true;
    public GameObject notifyPad;
    public Text nextStatusText;
    public Text newStatusText;
    public Text oldStatusText;
    public Text congratsText;
    public Text evolutionLevelText;
    public Text keepStatusText;
    public Text lossStatusText;
    
    private GameObject pet;

    private int currentStep;

    private int previousStep = 0;

    private void Start()
    {
        initialMax = max;
        currentEvo = 1;
        pet = GameObject.FindWithTag("pets");
        stepAverage = stepAverageGoal;
        stepCounter = GetComponent<StepCounter>();
        Timer();
    }

    private void Update()
    {
        //steps = 
        //currentStep = stepCounter.GetSteps();

        currentStep = TImer.TimerCurrentStep;

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
                Debug.Log((int)timer);
                Debug.Log("step average"); 
                Debug.Log(stepAverage);


                //Debug.Log("current step");

                //Debug.Log(currentStep);
                //Debug.Log("previous step");
                //Debug.Log(previousStep);
                //stepAverage = currentStep - previousStep;
                //previousStep = currentStep;
                //Debug.Log("step average");
                //Debug.Log(stepAverage);
                if (stepAverage >= stepAverageGoal)
                {
                    stepAverage = 18;
                    fill = true;
                    increase = true;
                    decrease = false;
                }
                else if (stepAverage < stepAverageGoal)
                {
                    stepAverage = 30;
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
