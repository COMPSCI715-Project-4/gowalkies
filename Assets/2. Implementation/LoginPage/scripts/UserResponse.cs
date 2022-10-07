using UnityEngine;

[System.Serializable]
public class UserResponse
{
    public UserInfo data;

    public UserResponse(UserInfo info)
    {
        this.data = info;
    }

    public static UserResponse CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<UserResponse>(jsonString);
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
