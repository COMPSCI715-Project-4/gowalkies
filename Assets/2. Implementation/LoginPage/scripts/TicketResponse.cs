using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TicketResponse
{
    public IEnumerable<Ticket> data;

    public static TicketResponse CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<TicketResponse>(jsonString);
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
