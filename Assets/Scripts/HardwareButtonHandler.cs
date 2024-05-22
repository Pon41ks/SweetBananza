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

            if (EventManager.isGameOver)
            {
                GameManager.Instance.OpenMenu();
                Debug.Log("sssssssss");
            }


            BackButtonPressed?.Invoke();


        }




    }
}

