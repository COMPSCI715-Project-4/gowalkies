using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{

    public GameObject panel;


    public void TogglePanelFunc()
    {
        //Debug.Log(panel.activeSelf); 
        panel.SetActive(!panel.activeSelf);
    } 
}
