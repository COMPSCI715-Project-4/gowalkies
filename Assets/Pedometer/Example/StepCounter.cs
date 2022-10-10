/* 
*   Pedometer
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace PedometerU.Tests {

    using UnityEngine;
    using UnityEngine.UI;

    public class StepCounter : MonoBehaviour {

        public Text stepText, distanceText;
        private Pedometer pedometer;
        public static int currentSteps = 0;
        public static float currentDistance = 0; 


        public void startStep()
        {
            // Create a new pedometer
            pedometer = new Pedometer(OnStep);
            // Reset UI
            OnStep(0, 0);
        }

        private void OnStep (int steps, double distance) {
            // Display the values // Distance in feet
            stepText.text = steps.ToString();
            distanceText.text = distance.ToString("F1") + "m";
            currentSteps = steps;
            currentDistance = float.Parse(distance.ToString("F1")); 
        }

        private void OnDisable () {
            // Release the pedometer
            pedometer.Dispose();
            pedometer = null;
        }

        public int GetSteps()
        { 
            return currentSteps;
        }

    }
}