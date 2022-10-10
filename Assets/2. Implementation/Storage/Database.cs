using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Database
{
    static string path;

    public Database()
    {
        path = Path.Combine(Application.persistentDataPath, "userinfo.json");
    }

    public void Store(UserInfo info)
    {
        File.WriteAllText(path, info.ToJson());
    }

    public string Token()
    {
        string data = File.ReadAllText(path);
        UserInfo info = UserInfo.CreateFromJSON(data);
        return info.token;
    }

    public void UpdateLevel(uint level)
    {
        string data = File.ReadAllText(path);
        UserInfo info = UserInfo.CreateFromJSON(data);
        info.pet.level = level;
        File.WriteAllText(path, info.ToJson());
    }
}