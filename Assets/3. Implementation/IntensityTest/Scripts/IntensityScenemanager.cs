using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntensityScenemanager : MonoBehaviour
{
    [SerializeField]
    private GameObject startPanel;
    [SerializeField]
    private GameObject testPanel;


    public void loadChoiceMenuScene()
    {
        SceneManager.LoadScene("ChoiceScene");
    }

    public void startPanelOpen()
    {
        testPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void testPanelOpen()
    {
        testPanel.SetActive(true);
        startPanel.SetActive(false);
    }
}
