using UnityEngine;

[System.Serializable]
public class PetResponse
{
    public Pet data;

    public PetResponse(Pet info)
    {
        this.data = info;
    }

    public static PetResponse CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PetResponse>(jsonString);
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
