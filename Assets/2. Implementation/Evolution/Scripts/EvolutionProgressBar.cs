using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UIWidgets;
using PedometerU.Tests;
using UnityEngine.XR.ARFoundation.Samples;

public class EvolutionProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    public float fillSpeed = 0.5f;
    public static int currentEvo;
    [SerializeField]
    private float timer = 0.01f;

    public float max = 30;
    private bool fill = true;
    private bool paused = true;
    private float initialMax;
    private float[] increaseRatio;
    // Step average variables
    private int steps = 0;
    public int stepAverageGoal;
    private int timeframe = 10;
    private bool increase = false;
    private bool decrease = false;
    private float stepAverage;
    private bool keepTiming = true;
    public GameObject notifyPad;
    public GameObject finishPanel;
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
    private string token;


    [SerializeField]
    private Text currentStepText;
    [SerializeField]
    private Text averageStepText;

    [SerializeField]
    private GameObject[] pets;
    [SerializeField]
    private Camera mainCamera;

    public static bool inEvoGame;
    public static int currentLevel = 0;


    private void Start()
    {
        Database database = new Database();
        token = database.Token();
    }

    public void startGame()
    {
        //The stepGoalAverage comes from the Intensity Test
        stepAverageGoal = testAverageStep.averageStep;
        inEvoGame = true;
        increaseRatio = new float[] { 10f, 20f, 30f };
        averageStepText.text = stepAverageGoal.ToString();

        initialMax = max;
        currentEvo = 1;
        currentLevel = 0; 
        stepAverage = stepAverageGoal;

        //The following code is the timer for calculating the current step
        //Update function will call every 20 seconds
        Timer();
        gameStart = true;
        t.Elapsed += new ElapsedEventHandler(handleUpdate);
        t.Interval = 10000;
        t.Start();

    }


    // This function calculates the average step
    private void handleUpdate(object source, ElapsedEventArgs e)
    {
        currentStep = StepCounter.currentSteps;
        stepAverage = currentStep - previousStep;
        currentStepText.text = stepAverage.ToString();
        previousStep = currentStep;
    }

    public void SaveStatus()
    {
        inEvoGame = false;

        StartCoroutine(UpdateRankHandler(token, currentEvo, currentStep, StepCounter.currentDistance));
    }

    private void Update()
    {
        pet = GameObject.FindGameObjectWithTag("pets");
        if (gameStart)
        {
            if (timer < max && keepTiming)
            {
                // We ignore the first timeframe and check if the time is a multiple of the timeframe e.g. 20
                if ((int)timer % timeframe == 0)
                {
                    // If the player maintains the step average then the bar will increase
                    if (stepAverage >= stepAverageGoal)
                    {
                        fill = true;
                        increase = true;
                        decrease = false;
                    }
                    // Else if the player does not maintain the step average then the bar will decrease
                    else if (stepAverage < stepAverageGoal)
                    {
                        fill = false;
                        decrease = true;
                        increase = false;
                    }
                }
                // Update the progress slider, inceasing or decreasing
                if (increase)
                    progressSlider.value = timer / max;
                else if (decrease)
                    progressSlider.value = timer / max;
                Timer();

                // If the progress slider drops to 0 then reset the timer
                if (progressSlider.value == 0 && currentEvo != 1 && timer <= 0)
                    ResetTimer();
            }
            else if (timer >= max)
            {
                ResetTimer();
                ChangePets();
            }
            else if (currentEvo == 4 && fill != false)
            {
                timer = max - 0.01f;
                paused = true;
            }
        }


    }

    public void ChangePets()
    {
        if (pet != null)
        {
            if (fill == true)
            {
                //Destroy(pet);
                //currentLevel = currentEvo; 
            }
            else if (fill == false)
            {
                //Destroy(pet);
                //currentLevel = 1; 
            }
            Timer();
        }
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

    // Resets the timer and edits text objects
    public void ResetTimer()
    {
        timer = 0.01f;
        keepTiming = false;
        notifyPad.SetActive(true);

        GameObject statusPanel = notifyPad.transform.GetChild(2).gameObject;
        GameObject increaseImage = statusPanel.transform.GetChild(0).gameObject;
        GameObject decreaseImage = statusPanel.transform.GetChild(1).gameObject;

        // If the progress bar is increasing
        if (fill == true)
        {
            finishPanel.SetActive(false);
            progressSlider.value = 0f;
            increaseImage.SetActive(true);
            decreaseImage.SetActive(false);
            newStatusText.text = "Level " + (currentEvo + 1);
            oldStatusText.text = "Level " + currentEvo;
            congratsText.text = "Congratulations! Your pet has evolved to level " + (currentEvo + 1) + "!";
            if (currentEvo == 4)
            {
                notifyPad.SetActive(false);
                finishPanel.SetActive(true);
            }

            if (currentEvo >= 2)
            {
                nextStatusText.text = "Congrats you've reached max level!";
            }
            else
                nextStatusText.text = "Next Evolution Level: " + (currentEvo + 2);

        }
        // If the progress bar is decreasing
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

    // This function is called when the close panel button is pressed after gaining an item
    public void ClosePanel()
    {
        keepTiming = true;
        notifyPad.SetActive(false);

        // If the progress bar is increasing
        if (fill == true)
        {
            if (currentEvo < 4)
            {
                max += increaseRatio[currentEvo - 1];
                Destroy(pet);
                currentLevel = currentEvo;
                currentEvo++;
            }
        }
        // If the progress bar is decreasing
        else if (fill == false)
        {
            max = initialMax;
            Destroy(pet);
            currentLevel = 0;
            currentEvo = 1;
        }
        evolutionLevelText.text = "Level " + currentEvo;
    }

    IEnumerator UpdateRankHandler(string token, int level, int steps, float distance)
    {

        WWWForm form = new WWWForm();
        form.AddField("token", token);
        form.AddField("kind", "evolution");
        form.AddField("level", (level - 1).ToString());
        form.AddField("steps", steps.ToString());
        form.AddField("distance", distance.ToString());
        form.AddField("duration", TImer.TimerCurrentTime.ToString());

        UnityWebRequest www = UnityWebRequest.Post("http://82.157.148.219/rank/update", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            UserResponse resp = UserResponse.CreateFromJSON(www.downloadHandler.text);
            UserInfo info = resp.data;
            Debug.Log(info.ToJson());
            UserDetails.info = info;
            Database db = new Database();
            db.Store(info);
        }
    }
}