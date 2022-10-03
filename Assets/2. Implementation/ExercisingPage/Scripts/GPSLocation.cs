using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GPSLocation : MonoBehaviour
{

    public Text GPSStatus;
    public Text LatitueValue;
    public Text longituteValue;
    public Text AltitudeValue;
    public Text HorizontalAccuracyValue;
    public Text TimeStampValue;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPSLoc()); 
    }

    IEnumerator GPSLoc()
    {
        //check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            yield break; 
        }


        //start servce befor querying location
        Input.location.Start();

        //wat until service initialize

        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;


        }

        //if service didnot init in 20 sec
        if (maxWait < 1)
        {
            GPSStatus.text = " Time out"; 
            yield break; 
        }

        //connection failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus.text = "Unable to determine device location";
            yield break; 
        }
        else
        {

            //Aces Granted
            GPSStatus.text = "Running";
            InvokeRepeating("UpdateGPSData", 0.5f, 1f); 

        }



    }//end of GPSLoc

    private void UpdateGPSData()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            GPSStatus.text = "Running";
            LatitueValue.text = Input.location.lastData.latitude.ToString();
            longituteValue.text = Input.location.lastData.longitude.ToString();
            AltitudeValue.text = Input.location.lastData.altitude.ToString();
            HorizontalAccuracyValue.text = Input.location.lastData.horizontalAccuracy.ToString();
            TimeStampValue.text = Input.location.lastData.timestamp.ToString();

        }
        else
        {
            //service is stopped
            GPSStatus.text = "Stop"; 

        }
    }//end of UpdateGPSLoc


}//end of GPSLocation
