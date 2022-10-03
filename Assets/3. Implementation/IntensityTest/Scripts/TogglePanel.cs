using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{

    public GameObject panel;


    public void TogglePanelFunc()
    {
        panel.SetActive(!panel);
    } 
}
