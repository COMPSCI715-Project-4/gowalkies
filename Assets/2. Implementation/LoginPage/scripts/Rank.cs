using UnityEngine;

[System.Serializable]
public class Rank
{
    public string username;
    public uint level;
    public uint highest_steps;

    public Rank(string username, uint level, uint highest_steps)
    {
        this.username = username;
        this.level = level;
        this.highest_steps = highest_steps;
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
