using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIWidgets; 

public class LoginSceneMannager : MonoBehaviour
{
    [SerializeField]
    private GameObject loginPage;
    [SerializeField]
    private GameObject SignUpPage;

    [SerializeField]
    private GameObject middleImage; 

    [SerializeField]
    private Popup signUpNotify;



    public void SignUpNotify()
    {
        middleImage.SetActive(false); 
        signUpNotify.Clone().Show("You have Signed Up Sucessfully!");

    }
    public void OpenSignUpPage()
    {
        middleImage.SetActive(true); 
        loginPage.SetActive(false);
        SignUpPage.SetActive(true); 
    }

    public void CloseSignUpPage()
    {
        loginPage.SetActive(true);
        SignUpPage.SetActive(false); 
    }
}
