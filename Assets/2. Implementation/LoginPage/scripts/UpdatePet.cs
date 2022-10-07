using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UpdatePet : MonoBehaviour
{

    public uint level;
    public string token;

    public UpdatePet(uint level, string token)
    {
        this.level = level;
        this.token = token;
    }

    
    void Update()
    {
        StartCoroutine(UpdateHandler());
    }

    // Update is called once per frame
    IEnumerator UpdateHandler()
    {

        WWWForm form = new WWWForm();
        form.AddField("token", this.token);
        form.AddField("level", this.level.ToString());

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/pet/update", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            PetResponse resp = PetResponse.CreateFromJSON(www.downloadHandler.text);
            Pet info = resp.data;
            Debug.Log(info.ToJson());
        }
    }
}
