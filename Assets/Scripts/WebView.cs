using UnityEngine;
using UnityEngine.UI;

namespace UISwitcher
{
    public class WebView : MonoBehaviour
    {

        [SerializeField] private WebViewObject webViewObject; // Ссылка на компонент WebViewObject
        [SerializeField] private GameObject webPanel;

        public void CreateWebView()
        {
            if (webViewObject == null)
            {
                // Создаем новый объект WebViewObject
                GameObject webViewGameObject = new GameObject("WebViewObject");

                RectTransform webViewRectTransform = webViewGameObject.AddComponent<RectTransform>();
                // Сделать WebView дочерним элементом Canvas
                webViewObject = webViewGameObject.AddComponent<WebViewObject>();


                // Инициализируем WebViewObject с обработчиками событий
                webViewObject.Init(
                    cb: (msg) =>
                    {
                        Debug.Log($"CallFromJS[{msg}]");
                    // При необходимости можно добавить обработку сообщений от JavaScript.
                },
                    err: (msg) =>
                    {
                        Debug.Log($"CallOnError[{msg}]");
                    },
                    started: (msg) =>
                    {
                        Debug.Log($"CallOnStarted[{msg}]");
                    },
                    hooked: (msg) =>
                    {
                        Debug.Log($"CallOnHooked[{msg}]");
                    },
                    ld: (msg) =>
                    {
                        Debug.Log($"CallOnLoaded[{msg}]");
                        webViewObject.SetVisibility(true);
                    }
                );

                // Загружаем URL в WebViewObject
                webViewObject.LoadURL("https://www.Google.com");
                // Устанавливаем видимость WebViewObject в false
                webViewObject.SetVisibility(true);
                webViewObject.SetMargins(0, 260, 0, 0);
                if (webViewObject.gameObject.activeInHierarchy)
                {
                    webPanel.SetActive(true);
                }
                HardwareButtonHandler.instance.SetWebView(webViewObject);
                
            }


            else
            {
                Debug.LogError("Panel object is not assigned!");
            }
        }


        public void CloseWindow()
        {
            if (webViewObject != null)
            {
                // Деактивируем окно
                Destroy(webViewObject.gameObject);
                webPanel.SetActive(false);
            }
            else
            {
                Debug.LogError("Window object is not assigned!");
            }
        }
    }

}
