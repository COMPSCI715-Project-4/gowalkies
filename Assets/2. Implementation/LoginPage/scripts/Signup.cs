using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UIWidgets; 

public class Signup: MonoBehaviour
{
    [SerializeField]
    private InputField UserNameInput;
    [SerializeField]
    private InputField PasswordInput;
    [SerializeField]
    private InputField ComfirmPasswordInput;

    [SerializeField]
    private Popup SignUpNotifyPopUp;

    [SerializeField]
    private GameObject StartIntensityPanel;

    [SerializeField]
    private GameObject SignUpPanel;

    private string userName;
    private string password;
    private string confirmPassword;

    // Start is called before the first frame update
    public void StartSignup()
    {
        userName = UserNameInput.text;
        password = PasswordInput.text;
        confirmPassword = ComfirmPasswordInput.text;
        StartCoroutine(SignupHandler(userName, password));
    }

    // Update is called once per frame
    IEnumerator SignupHandler(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post("http://82.157.148.219/signup", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            SignUpNotifyPopUp.Clone().Show("Notification", www.error);
        }
        else
        {
            UserResponse resp = UserResponse.CreateFromJSON(www.downloadHandler.text);
            UserInfo info = resp.data;
            Debug.Log(info.ToJson());
            UserDetails.info = info;
            Database db = new Database();
            db.Store(info);
            SignUpPanel.SetActive(false); 
            StartIntensityPanel.SetActive(true);
        }
    }
}
