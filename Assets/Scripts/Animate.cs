using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Animate : MonoBehaviour
{
    [SerializeField] private Image pausePanel;
    [SerializeField] private GameObject one;
    [SerializeField] private GameObject two;
    [SerializeField] private GameObject three;
    public bool startPause;

    private void Awake()
    {
        pausePanel = GetComponent<Image>();
    }

    private void Update()
    {
        if (startPause)
        {
            pausePanel.enabled = true;
            StartCoroutine(BeforeUnpause());
            startPause = false;

        }
        
    }
    

    private IEnumerator BeforeUnpause()
    {
        
        three.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        three.SetActive(false); 
        two.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        two.SetActive(false);
        one.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        one.SetActive(false);
        pausePanel.enabled = false;
        Time.timeScale = 1f;
        yield return new WaitForEndOfFrame();
    }
}
