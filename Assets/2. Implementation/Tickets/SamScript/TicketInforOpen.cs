using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketInforOpen : MonoBehaviour
{
    public GameObject Panel;

    public void closePanel()
    {
        Panel.SetActive(false);

    }
    public void openPanel()
    {
        Panel.SetActive(true);
    }
}
