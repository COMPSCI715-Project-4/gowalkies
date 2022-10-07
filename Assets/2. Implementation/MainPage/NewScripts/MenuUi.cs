using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{

    public void GoToTicketScene()
    {
        SceneManager.LoadScene("TicketScene");
    }

    public void GoToEvolutionScene()
    {
        SceneManager.LoadScene("EvolutionScene");
    }

    public void GoToStatusScene()
    {
        SceneManager.LoadScene("SocialStandingScene");
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }


    public void GoBackToChoiceMenu()
    {
        SceneManager.LoadScene("ChoiceScene");

    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }

}
