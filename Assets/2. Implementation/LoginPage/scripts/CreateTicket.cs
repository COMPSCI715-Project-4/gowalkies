using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CreateTicket : MonoBehaviour
{
    public string token;
    public string description;
    public uint level;
    public ulong expiresAt;

    // Start is called before the first frame update
    void Create()
    {
        StartCoroutine(CreateHandler());
    }

    // Update is called once per frame
    IEnumerator CreateHandler()
    {
        WWWForm form = new WWWForm();
        form.AddField("token", token);
        form.AddField("description", description);
        form.AddField("level", level.ToString());
        form.AddField("expires_at", expiresAt.ToString());


        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/ticket/create", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            TicketResponse resp = TicketResponse.CreateFromJSON(www.downloadHandler.text);
            IEnumerable<Ticket> tickets = resp.data;

            foreach (Ticket ticket in tickets)
            {
                Debug.Log(ticket.ToJson());
            }
        }
    }
}
