﻿using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UnityEngine.XR.ARFoundation.Samples
{
    public class BackButton : MonoBehaviour
    {
        [SerializeField]
        GameObject m_BackButton;
        public GameObject backButton
        {
            get => m_BackButton;
            set => m_BackButton = value;
        }

        void Start()
        {
            if (Application.CanStreamedLevelBeLoaded("MainMenu"))
                m_BackButton.SetActive(true);
        }

        void Update()
        {
            if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
                BackButtonPressed();
        }

        public void BackButtonPressed()
        {
            Debug.Log("back Btn Pressed");
            if (Application.CanStreamedLevelBeLoaded("MainMenu"))
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
