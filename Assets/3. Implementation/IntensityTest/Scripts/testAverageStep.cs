using System.Collections;
using System.Collections.Generic;
using PedometerU;
using UnityEngine;
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


    public void startStep()
    {
        // Create a new pedometer
        Debug.Log("step count start");
        pedometer = new Pedometer(OnStep);

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
            averageStep = Mathf.FloorToInt(int.Parse(stepsText.text) / 6);
            Debug.Log(averageStep);
            averageStepText.text = averageStep.ToString();
            averageStepNotify.SetActive(true);

        }
        else
        {
            averageStepNotify.SetActive(true);
            averageStepText.text = "Your average step is Null";

        }

    }


}
