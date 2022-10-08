using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Networking;

public class CreateRankText : MonoBehaviour
{

    public GameObject listItemPrefab;
    public Transform listItemHolder;
    // Start is called before the first frame update
    //void Start()
    //{

    //}
    public List<Rank> ranks;

    public void CreateRankList()
    {
        GameObject[] itemList = GameObject.FindGameObjectsWithTag("items");

        for(int i =0; i < itemList.Length; i++)
        {
            Destroy(itemList[i]);
        }

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

            //Debug.Log(www.downloadHandler.text);
            RankResponse resp = RankResponse.CreateFromJSON(www.downloadHandler.text);

            
            foreach (Rank rank in resp.data)
            {
                Debug.Log(rank.ToJson());
                
                listItemPrefab.transform.GetChild(0).GetComponentInChildren<Text>().text = rank.username;
                listItemPrefab.transform.GetChild(1).GetComponentInChildren<Text>().text = rank.level.ToString();
                listItemPrefab.transform.GetChild(2).GetComponentInChildren<Text>().text = rank.highest_steps.ToString();
                Instantiate(listItemPrefab, listItemHolder);

            }
        }
    }
}
