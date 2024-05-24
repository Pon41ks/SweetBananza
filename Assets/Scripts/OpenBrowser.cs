using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBrowser : MonoBehaviour
{
    public void OpenUrl()
    {
        Application.OpenURL("https://google.com/");
    }
}
