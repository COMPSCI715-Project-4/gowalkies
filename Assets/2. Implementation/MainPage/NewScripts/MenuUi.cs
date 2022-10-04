using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    public void GoToTicketInstructions()
    {
        SceneManager.LoadScene("TicketInstructions");
    }

    public void GoToTicketScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToEvoInstructions()
    {
        SceneManager.LoadScene("EvoInstructions");
    }

    public void GoToEvolutionScene()
    {
        SceneManager.LoadScene("EvolutionScene");
    }

    public void GoToStatusInstructions()
    {
        SceneManager.LoadScene("StatusInstructions");
    }

    public void GoToStatusScene()
    {
        SceneManager.LoadScene("SocialStandingScene");
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
