using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{

    [SerializeField] private Slider slider;
    
    private void Start()
    {
        StartCoroutine(AsyncLoading());
    }
  

    IEnumerator AsyncLoading()
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(1);

        loadAsync.allowSceneActivation = false;
        while (!loadAsync.isDone)
        {
            slider.value = loadAsync.progress;
            if (loadAsync.progress > .89f && !loadAsync.allowSceneActivation)
            {

                yield return new WaitForSeconds(1);
                loadAsync.allowSceneActivation = true;
            }
            yield return null;
        }

    }
}
