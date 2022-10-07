using UnityEngine;

[System.Serializable]
public class UserInfo
{
    public string token;
    public string id;
    public string username;
    public string email;
    public Ticket[] tickets;
    public Pet pet;

    public UserInfo(string token, string id, string username, Ticket[] tickets, Pet pet)
    {
        this.token = token;
        this.id = id;
        this.username = username;
        this.tickets = tickets;
        this.pet = pet;
    }

    public static UserInfo CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<UserInfo>(jsonString);
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
