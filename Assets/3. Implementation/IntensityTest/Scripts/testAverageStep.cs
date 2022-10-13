using System.Collections;
using System.Collections.Generic;
using PedometerU;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class testAverageStep : MonoBehaviour
{

    [SerializeField]
    private Text stepsText;
    [SerializeField]
    private Text distanceText;
    [SerializeField]
    private Text averageStepText;
    [SerializeField]
    private GameObject averageStepNotify;

    private Pedometer pedometer;
    private float timer;


    public static int averageStep;
    string token;


    private void Start()
    {
        Database db = new Database();
        token = db.Token();
        pedometer = new Pedometer(OnStep);
    }

    public void startStep()
    {
        // Create a new pedometer
        Debug.Log("step count start");


        // Reset UI
        OnStep(0, 0);
    }


    private void OnStep(int steps, double distance)
    {
        // Display the values // Distance in feet
        
        stepsText.text = steps.ToString();
        distanceText.text = distance.ToString("F1") + "m";


    }

    public void closeStep()
    {
        // Release the pedometer
        pedometer.Dispose();
        pedometer = null;

    }



    public void testAverageSteps()
    {
        if (stepsText.text != null)
        {
            averageStep = Mathf.FloorToInt(int.Parse(stepsText.text) / 4);

            Debug.Log(averageStep);
            averageStepText.text = averageStep.ToString();
            averageStepNotify.SetActive(true);

            StartCoroutine(UpdateHandler());
        }
        else
        {
            averageStepNotify.SetActive(true);
            averageStepText.text = "Your average step is Null";

        }
    }

    IEnumerator UpdateHandler()
    {
        WWWForm form = new WWWForm();
        form.AddField("token", token);
        form.AddField("average_steps", averageStep.ToString());

        UnityWebRequest www = UnityWebRequest.Post("http://82.157.148.219/intensity", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {

            Debug.Log(www.error);
        }
    }
}
