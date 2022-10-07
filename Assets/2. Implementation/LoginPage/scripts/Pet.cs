using UnityEngine;

[System.Serializable]
public class Pet
{
    public string id;
    public string birthday;
    public uint level;

    public Pet(string id, string birthday, uint level)
    {
        this.id = id;
        this.birthday = birthday;
        this.level = level;
    }

    public static Pet CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Pet>(jsonString);
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
