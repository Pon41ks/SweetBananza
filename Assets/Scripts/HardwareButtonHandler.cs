using UnityEngine;
using UnityEngine.InputSystem;

namespace UISwitcher
{
    public class HardwareButtonHandler : MonoBehaviour
    {
        [SerializeField] private InputAction backButtonAction;
        [SerializeField] private Settings settings;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject closePanel;
        private WebViewObject webView; // Используем тип WebViewObject для ссылки на WebView

        public static HardwareButtonHandler instance;
        public delegate void ButtonPress();
        public event ButtonPress BackButtonPressed;

        private void Awake()
        {
            // Получаем ссылку на Settings
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

        // Метод для установки ссылки на WebView
        public void SetWebView(WebViewObject webViewObject)
        {
            webView = webViewObject;
            Debug.Log(webView);
        }

        private void BackButtonDetected()
        {
            // Проверяем, что webView не равен null и WebViewObject активен
            if (webView != null && webView.gameObject.activeSelf)
            {
                if (closePanel != null)
                {
                    closePanel.SetActive(false);
                }
                Destroy(webView.gameObject);
            }
            else if (EventManager.isGameOver)
            {
                GameManager.Instance.OpenMenu();
                Debug.Log("Game over menu opened");
            }

            BackButtonPressed?.Invoke();
        }
    }
}
