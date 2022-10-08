using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FetchRank : MonoBehaviour
{
    void Fetch()
    {
        StartCoroutine(FetchHandler());

    }

    // Update is called once per frame
    IEnumerator FetchHandler()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://82.157.148.219/rank");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            RankResponse resp = RankResponse.CreateFromJSON(www.downloadHandler.text);
            IEnumerable<Rank> ranks = resp.data;

            foreach (Rank rank in ranks)
            {
                Debug.Log(rank.ToJson());
            }
        }
    }
}
