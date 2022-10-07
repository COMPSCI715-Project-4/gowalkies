using UnityEngine;

[System.Serializable]
public class Rank
{
    public string username;
    public uint level;

    public Rank(string username, uint level)
    {
        this.username = username;
        this.level = level;
    }

    public static Rank CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Rank>(jsonString);
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
