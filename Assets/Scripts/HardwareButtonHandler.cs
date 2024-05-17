using UnityEngine;
using UnityEngine.InputSystem;

namespace UISwitcher
{
    public class HardwareButtonHandler : MonoBehaviour
    {
        [SerializeField] private InputAction backButtonAction;


        public static HardwareButtonHandler instance;
        public delegate void ButtonPress();
        public event ButtonPress BackButtonPressed;

        [SerializeField] private Settings settings;
        [SerializeField] private GameObject settingsPanel;
        private bool isSettingsOpen;

        private void Awake()
        {
            //settings = GetComponent<Settings>();
            settings = FindAnyObjectByType<Settings>();
            backButtonAction.Enable();
            backButtonAction.performed += _ => { BackButtonDetected(); };
            instance = this;
        }

        private void OnEnable()
        {
            backButtonAction.Enable();

        }

        private void OnDisable()
        {
            backButtonAction.Disable();

        }

        private void BackButtonDetected()
        {
            if (isSettingsOpen)
            {
                Time.timeScale = 1f;
                settingsPanel.SetActive(false);
                isSettingsOpen = false; // Устанавливаем флаг на false, так как панель закрыта
                Debug.Log("Settings panel closed");
            }
            else
            {
                isSettingsOpen = true; // Устанавливаем флаг на true, так как панель открывается
                Debug.Log("Settings panel opened");
                settingsPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            BackButtonPressed?.Invoke();
        }



    }
}

