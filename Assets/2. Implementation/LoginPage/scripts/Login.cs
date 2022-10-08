using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UIWidgets;

public class Login : MonoBehaviour
{
    [SerializeField]
    private InputField UsernameInput;
    [SerializeField]
    private InputField PasswordInput;

    [SerializeField]
    private GameObject loginPanel;
    [SerializeField]
    private GameObject signUpPanel;

    [SerializeField]
    private Popup notifyPopUp;

    [SerializeField]
    private GameObject IntensityPanel; 

    private string username;
    private string password; 

    // Start is called before the first frame update
    public void StartLogin()
    {
        username = UsernameInput.text;
        password = PasswordInput.text;
        StartCoroutine(LoginHandler(username, password));
    }

    // Update is called once per frame
    IEnumerator LoginHandler(string username, string password)
    {

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post("http://82.157.148.219/login", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {

            Debug.Log(www.error);
            notifyPopUp.Clone().Show("Notification", "Login is unsucessfull. \n Please check your username and password."); 

        }
        else
        {
            UserResponse resp = UserResponse.CreateFromJSON(www.downloadHandler.text);
            UserInfo info = resp.data;
            Debug.Log(info.ToJson());
            UserDetails.info = info;
            Database db = new Database();
            db.Store(info);
            loginPanel.SetActive(false);
            IntensityPanel.SetActive(true);

        }
    }
}
