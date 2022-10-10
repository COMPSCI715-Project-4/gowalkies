using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIWidgets;
using PedometerU.Tests;
using System.Timers;
using UnityEngine.Networking;

public class StatusProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    private float fillSpeed = 0.5f;
    public Text statusText;
    public static int currentStatus;
    [SerializeField]
    private float timer = 0.01f;
    public float max = 30;
    private bool fill = true;
    private bool paused = true;
    private float initialMax = 10;
    private float[] increaseRatio;
    // Step average variables
    private int steps = 0;

    [SerializeField]
    private float stepAverageGoal;
    private int timeframe = 20;
    private bool increase = false;
    private bool decrease = false;
    private float stepAverage;
    private StepCounter stepCounter;

    public static List<string> statusNames = new List<string> { "Pet Newbie", "Pet Lover", "Pet Master", "Pet Legend" };
    private List<string> unlocked = new List<string> { "Pet Newbie" };
    private bool keepTiming = true;
    public GameObject notifyPad;
    public GameObject finishPanel;
    public Text nextStatusText;
    public Text newStatusText;
    public Text oldStatusText;
    public Text congratsText;
    public Dropdown statusesUnlocked;
    public Text keepStatusText;
    public Text lossStatusText;
    private string token;

    private Timer t = new Timer();

    private int currentStep;

    private int previousStep = 0;
    private bool gameStart = false;

    [SerializeField]
    public GameObject pet;


    //private void Start()
    //{
    //    stepAverageGoal = 29; 
    //    initialMax = max;
    //    currentStatus = 0;
    //    statusText.text = statusNames[currentStatus];
    //    statusesUnlocked.ClearOptions();
    //    statusesUnlocked.AddOptions(unlocked);
    //    stepAverage = stepAverageGoal;
    //    stepCounter = GetComponent<StepCounter>();
    //    Timer();



    //}

    private void Start()
    {
        Database database = new Database();
        token = database.Token();
        StartCoroutine(UpdateRankHandler(token, 0, 0, 0));
    }



    public void startGame()
    {
        //set the personal step goal
        //come from the Intensity Test
        stepAverageGoal = testAverageStep.averageStep; //this should be uncommented when we exported the app. Link to the average eintensity test

        //this can be accessed and changed from the inspector
        //testing purpose
        //stepAverageGoal = 19;
        initialMax = max;
        increaseRatio = new float[] { 10f, 20f, 30f };
        currentStatus = 0;
        statusText.text = statusNames[currentStatus];
        statusesUnlocked.ClearOptions();
        statusesUnlocked.AddOptions(unlocked);
        stepAverage = stepAverageGoal;
        stepCounter = GetComponent<StepCounter>();
        Instantiate(pet); 
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


        currentStep = stepCounter.GetSteps(); //should be uncommented when we want to exported the app
        stepAverage = currentStep - previousStep;
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
                if (progressSlider.value == 0 && currentStatus != 0 && timer <= 0)
                    ResetTimer();
            }

            else if (timer >= max)
            {
                ResetTimer();
            }
            

        }
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

        if (fill == true)
        {
            finishPanel.SetActive(false);
            progressSlider.value = 0f;
            increaseImage.SetActive(true);
            decreaseImage.SetActive(false);
            if (currentStatus == 3)
            {
                notifyPad.SetActive(false);
                finishPanel.SetActive(true);
            }
            newStatusText.text = statusNames[currentStatus + 1];
            oldStatusText.text = statusNames[currentStatus];
            congratsText.text = "Congratulations! You have increased your status to " + statusNames[currentStatus + 1] + "!";

            if (currentStatus >= 2)
            {
                nextStatusText.text = "Congrats you've reached the highest social standing!";
            }
            else
                nextStatusText.text = "Next Status: " + statusNames[currentStatus + 2];
            StartCoroutine(UpdateRankHandler(token, currentStatus, currentStep, StepCounter.currentDistance));

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
            StartCoroutine(UpdateRankHandler(token, currentStatus, currentStep, StepCounter.currentDistance));
        }
    }

    public void closePanel()
    {
        Debug.Log("close panel"); 
        keepTiming = true;
        notifyPad.SetActive(false);

        if (fill == true)
        {
            max += increaseRatio[currentStatus];
            if (currentStatus < 4)
                currentStatus++;
            statusText.text = statusNames[currentStatus];

            // Add the new status as an option for the dropdown menu
            unlocked.Add(statusNames[currentStatus]);
            statusesUnlocked.ClearOptions();
            statusesUnlocked.AddOptions(unlocked);
            statusesUnlocked.captionText.text = statusNames[currentStatus];
            StartCoroutine(UpdateRankHandler(token, currentStatus, currentStep, StepCounter.currentDistance));
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
            StartCoroutine(UpdateRankHandler(token, currentStatus, currentStep, StepCounter.currentDistance));
        }
        fill = true;
        stepAverage = stepAverageGoal;
    }

    public void SaveStatus()
    {
        StartCoroutine(UpdateRankHandler(token, currentStatus, currentStep, StepCounter.currentDistance));
    }

    IEnumerator UpdateRankHandler(string token, int level, int steps, float distance)
    {

        WWWForm form = new WWWForm();
        form.AddField("token", token);
        form.AddField("kind", "rank");
        form.AddField("level", level.ToString());
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
            Debug.Log("here");
            UserResponse resp = UserResponse.CreateFromJSON(www.downloadHandler.text);
            UserInfo info = resp.data;
            Debug.Log(info.ToJson());
            UserDetails.info = info;
            Database db = new Database();
            db.Store(info);
        }
    }

    private void OnDestroy()
    {
        
        StartCoroutine(UpdateRankHandler(token, currentStatus, currentStep, StepCounter.currentDistance));
    }

}
