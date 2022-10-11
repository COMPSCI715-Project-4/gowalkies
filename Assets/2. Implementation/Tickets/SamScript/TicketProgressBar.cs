using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UIWidgets;
using PedometerU.Tests;


public class TicketProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    public float fillSpeed = 0.5f;
    public static int currentEvo;
    [SerializeField]
    private float timer = 0.01f;
    [SerializeField]
    private float max = 30;

    private bool fill = true;
    private bool paused = false;
    private float initialMax = 10;
    private bool once = false;
    public static List<int> gained = new List<int>();

    private bool keepTiming = true;
    public GameObject notifyPad;
    public GameObject panel;
    private float[] increaseRatio;
    public Text nextStatusText;
    public Text newStatusText;
    public Text oldStatusText;
    public Text congratsText;
    public Text evolutionLevelText;
    public Text keepStatusText;
    public Text lossStatusText;
    private Timer t = new Timer();

    // Step average variables
    private int steps = 0;
    public static int extra_step = 0;

    [SerializeField]
    public float stepAverageGoal;

    private int timeframe = 10;
    private bool increase = false;
    private bool decrease = false;
    [SerializeField]
    private float stepAverage;

    // Step Counter variables
    private StepCounter stepCounter;
    private int previousStep = 0;
    private bool gameStart = false;

    [SerializeField]
    private GameObject pet;
    private int currentStep;
    private string token;


    private void Start()
    {
        // initialMax = max;
        // currentEvo = 1;
        // pet = GameObject.FindWithTag("pets");
        fill = false;
        
        Database database = new Database();
        token = database.Token();

    }

    public void GameStart()
    {
        increaseRatio = new float[] { 10f, 20f, 30f };
        fill = true;
        // Timer();
        // stepAverage = stepAverageGoal;
        

        //set the personal step goal
        //come from the Intensity Test
        stepAverageGoal = testAverageStep.averageStep; //this should be uncommented when we exported the app. Link to the average eintensity test

        //this can be accessed and changed from the inspector
        //testing purpose
        //stepAverageGoal = 19;

        initialMax = max;
        currentEvo = 1;
        stepAverage = stepAverageGoal;
        stepCounter = GetComponent<StepCounter>();

        //following codes are the timer for calculating the current step
        //handle update function will call every 20 seconds
        Timer();
        gameStart = true;
        t.Elapsed += new ElapsedEventHandler(handleUpdate);
        t.Interval = 10000;
        t.Start();
    } 


    //calculate the average step
    private void handleUpdate(object source, ElapsedEventArgs e)
    {

        //testing purpose
        //step increment one by one sec
        //currentStep = TImer.TimerCurrentStep;


        currentStep = stepCounter.GetSteps(); //should be uncommented when we want to exported the app
        //Debug.Log(stepAverage);
        Debug.Log(extra_step.ToString()); 

        stepAverage = currentStep - previousStep + extra_step;
        
        previousStep = currentStep;

        extra_step = 0; 
        //Debug.Log(stepAverage); 
    }

    private void Update()
    {   
        if (gameStart)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                stepAverage = stepAverageGoal;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                stepAverage = 0;
            }

            // 20 seconds check point 
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
                {
                    progressSlider.value = timer / max;
                }
                
                else if (decrease)
                {
                    progressSlider.value = timer / max;
                }

                Timer();

                if (progressSlider.value == 0 && currentEvo != 1 && timer <= 0)
                {
                    ResetTimer();
                }
                // Reset 
                //extra_step = 0;
            }

            else if (timer >= max)
            {
                ResetTimer();
            }

            else if (currentEvo > 4 && fill != false)
            {
                timer = max - 0.01f;
                paused = true;
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
        {
            pet = GameObject.FindWithTag("pets");
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

    public void ResetTimer()
    {   
        timer = 0.01f;
        keepTiming = false;
        notifyPad.SetActive(true);

        GameObject statusPanel = notifyPad.transform.GetChild(2).gameObject;
        GameObject ticket_1 = statusPanel.transform.GetChild(0).gameObject;
        GameObject ticket_2 = statusPanel.transform.GetChild(1).gameObject;
        GameObject ticket_3 = statusPanel.transform.GetChild(2).gameObject;
        GameObject ticket_4 = statusPanel.transform.GetChild(3).gameObject;
        GameObject finish_btn = statusPanel.transform.GetChild(4).gameObject;
        

        if (fill == true)
        {
            progressSlider.value = 0f;
            if (currentEvo == 1 && once != true)
            {   
                finish_btn.SetActive(false);
                ticket_4.SetActive(false);
                ticket_1.SetActive(true);
                if (!gained.Contains(1))
                {
                    gained.Add(1);
                    once = true;
                }
            }

            else if (currentEvo == 2 && once != true)
            {   
                ticket_1.SetActive(false);
                ticket_2.SetActive(true);
                if (!gained.Contains(2))
                {
                    gained.Add(2);
                    once = true;
                }
            }
            else if (currentEvo == 3 && once != true)
            {
                ticket_2.SetActive(false);
                ticket_3.SetActive(true);    
                if (!gained.Contains(3))
                {
                    gained.Add(3);
                    once = true;
                }
            }
            else if (currentEvo == 4 && once != true)
            {
                ticket_3.SetActive(false);
                ticket_4.SetActive(true);    
                if (!gained.Contains(4))
                {
                    gained.Add(4);
                    once = true;
                }
            }

            newStatusText.text = "Level " + (currentEvo + 1);
            oldStatusText.text = "Level " + currentEvo;

            if (currentEvo == 5)
            {   
                ticket_4.SetActive(false);
                finish_btn.SetActive(true);
                congratsText.text = "Congratulations! \n Your have completed level " + (currentEvo) + "!" + "\n You completed all the levels";
            }
            else    
            {
                congratsText.text = "Congratulations! \n Your have completed level " + (currentEvo) + "!" + "\n You gained the ticket below.";
            }
            
            // For the nextStatusText
            if (currentEvo >= 2)
            {
                nextStatusText.text = "Congrats you've reached max level!";
            }
            else
            {
                nextStatusText.text = "Next Evolution Level: " + (currentEvo + 2);
            }
        }
        else if (fill == false)
        {
            progressSlider.value = max;
            // increaseImage.SetActive(false);
            // decreaseImage.SetActive(true);
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

            if (currentEvo < 4)
            {
                max += increaseRatio[currentEvo - 1];
            }
            if (currentEvo <= 4)
                currentEvo++;
                once = false;
            SaveStatus();
        }
        else if (fill == false)
        {
            max = initialMax;
            currentEvo = 1;
            SaveStatus();
        }
        evolutionLevelText.text = "Level " + currentEvo;
        fill = true;
        stepAverage = stepAverageGoal;
    }

    public void SaveStatus()
    {
        StartCoroutine(UpdateRankHandler(token, currentEvo, currentStep, StepCounter.currentDistance));
    }

    IEnumerator UpdateRankHandler(string token, int level, int steps, float distance)
    {

        WWWForm form = new WWWForm();
        form.AddField("token", token);
        form.AddField("kind", "ticket");
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

    //private void OnDestroy()
    //{
    //    StartCoroutine(UpdateRankHandler(token, currentEvo, currentStep, StepCounter.currentDistance));
    //}
}
