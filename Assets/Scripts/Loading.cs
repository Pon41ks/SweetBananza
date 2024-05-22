using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    [SerializeField]private Slider loadingSlider;
    private bool isLoadingIsFinished;

    private void Update()
    {
        if (isLoadingIsFinished)
        {
            StartCoroutine(Wait());
        }
        
        for (int i = 0; i < 10; i++)
        {
            loadingSlider.value = i;
            if (loadingSlider.value >= 0.9)
            {
                isLoadingIsFinished = true;
            }
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        Load();
    }

    private void Load()
    {

        SceneManager.LoadScene(1);
    }


}

