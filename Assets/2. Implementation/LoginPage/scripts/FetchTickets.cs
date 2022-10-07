using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FetchTickets : MonoBehaviour
{

    public string token;


    void Fetch()
    {
        StartCoroutine(FetchHandler());
    }

    // Update is called once per frame
    IEnumerator FetchHandler()
    {

        WWWForm form = new WWWForm();
        form.AddField("token", this.token);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/ticket/fetch", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            TicketResponse resp = TicketResponse.CreateFromJSON(www.downloadHandler.text);
            IEnumerable<Ticket> tickets = resp.data;

            foreach(Ticket ticket in tickets) {
                Debug.Log(ticket.ToJson());
            }
        }
    }
}
