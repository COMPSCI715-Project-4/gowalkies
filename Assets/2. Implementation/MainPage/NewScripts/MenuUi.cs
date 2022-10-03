using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    public void GoToTicketScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToEvolutionScene()
    {
        SceneManager.LoadScene("EvolutionScene");
    }

    public void GoToStatusScene()
    {
        SceneManager.LoadScene("SocialStandingScene");
    }

    public void LoadIntensityTestScene()
    {
        SceneManager.LoadScene("IntensityTestScene");
    }

    public void LoadLoginScene()
    {
        SceneManager.LoadScene("LoginScene");
    }

 
}
