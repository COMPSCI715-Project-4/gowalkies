using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RankResponse
{
    public IEnumerable<Rank> data;

    public static RankResponse CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<RankResponse>(jsonString);
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
