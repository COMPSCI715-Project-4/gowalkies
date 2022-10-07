using UnityEngine;

[System.Serializable]
public class Ticket
{
    public string description;
    public ulong expiresAt;
    public uint level;

    public Ticket(string description, ulong expiresAt, uint level)
    {
        this.description = description;
        this.expiresAt = expiresAt;
        this.level = level;
    }

    public static Ticket CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Ticket>(jsonString);
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
