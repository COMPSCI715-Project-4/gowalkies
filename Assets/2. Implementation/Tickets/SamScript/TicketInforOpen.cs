using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketInforOpen : MonoBehaviour
{   
    // variables
    public GameObject Panel;

    // Close the panel
    public void closePanel()
    {   
        Panel.SetActive(false);

    }

    // Open the panel
    public void openPanel()
    {
        Panel.SetActive(true);
    }
}
