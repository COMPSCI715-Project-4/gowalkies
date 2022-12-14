using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class TicketManager : MonoBehaviour
{   
    // Variables and Constants
    public GameObject statusPanel;
    public Text confirmText;
    public Button confirmBtn;
    public Button cancelBtn;
    private static List<GameObject> mytic = new List<GameObject>();
    private int[] extra_steps = {10, 20, 30, 40};
    private int remove;
    private bool confirm;

    GameObject ticket_1;
    GameObject ticket_2;
    GameObject ticket_3;
    GameObject ticket_4;
    

    // Start is called before the first frame update
    void Start()
    {   
        // Gain the tickets objects from the status panel
        ticket_1 = statusPanel.transform.GetChild(0).gameObject;
        ticket_2 = statusPanel.transform.GetChild(1).gameObject;
        ticket_3 = statusPanel.transform.GetChild(2).gameObject;
        ticket_4 = statusPanel.transform.GetChild(3).gameObject;
        ticket_1.GetComponent<Button>().onClick.AddListener(Onclick_1);
        ticket_2.GetComponent<Button>().onClick.AddListener(Onclick_2);
        ticket_3.GetComponent<Button>().onClick.AddListener(Onclick_3);
        ticket_4.GetComponent<Button>().onClick.AddListener(Onclick_4);
        // Add Listener
        confirmBtn.onClick.AddListener(Confirmed);
        cancelBtn.onClick.AddListener(Cancelled);
        // Add tickets to the wallet
        mytic.Add(ticket_1);
        mytic.Add(ticket_2);
        mytic.Add(ticket_3);
        mytic.Add(ticket_4);
        // Disappear the tickets
        ticket_1.SetActive(false);
        ticket_2.SetActive(false);
        ticket_3.SetActive(false);
        ticket_4.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {   
        ticket_1.SetActive(false);
        ticket_2.SetActive(false);
        ticket_3.SetActive(false);
        ticket_4.SetActive(false);

        // Update the tickets
        for (var i = 0; i < TicketProgressBar.gained.Count; i++) 
        {   
            int ticket_num = TicketProgressBar.gained[i];
            mytic[ticket_num-1].SetActive(true);
        }

        // Use the tickets

        if (confirm == true)
        {   
            // Remove the ticket
            TicketProgressBar.gained.Remove(remove);
            confirm = false;
        
            // Reduce the steps
            TicketProgressBar.extra_step += extra_steps[remove-1];
            //Debug.Log(TicketProgressBar.extra_step.ToString()); 
        }

        confirm = false;

    }

    // Button is clicked
    void Onclick_1()
    {
        confirmText.text = "Are you sure you want to use ticket 1?";
        remove = 1;
    }

    // Button is clicked
    void Onclick_2()
    {
        confirmText.text = "Are you sure you want to use ticket 2?";
        remove = 2;
    }

    // Button is clicked
    void Onclick_3()
    {
        confirmText.text = "Are you sure you want to use ticket 3?";
        remove = 3;
    }

    // Button is clicked
    void Onclick_4()
    {
        confirmText.text = "Are you sure you want to use ticket 4?";
        remove = 4;
    }

    // Button is clicked
    void Confirmed()
    {
        confirm = true;
    }

    // Button is clicked

    void Cancelled()
    {
        confirm = false;
    }

    // Distroy all the tickets
    private void OnDestroy()
    {
        ticket_1.SetActive(false);
        ticket_2.SetActive(false);
        ticket_3.SetActive(false);
        ticket_4.SetActive(false);
    }
}