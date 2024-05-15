using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGame : MonoBehaviour
{
    [SerializeField] private  Chest[] chests;
    [SerializeField] private GameObject openChest;
    [SerializeField] private GameObject openChest2;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject bonusPanel;

    private Animate animate;
    public bool isCorrectChest;

    private void OnEnable()
    {
        int index = Random.Range(0, 4);
        isCorrectChest = false;
        chests[index].hasTreasure = true;
        animate = FindAnyObjectByType<Animate>();

        content.SetActive(true);
        foreach (var chest in chests)
        {
            chest.gameObject.SetActive(true);
        }

    }

    private void Update()
    {
        if (Chest.isChoose)
        {
            Chest.isChoose = false;
            StartCoroutine(WinAnimation());
            
            
        }
    }
    private IEnumerator WinAnimation()
    {
        isCorrectChest = true;

        foreach (var chest in chests)
        {

            if (!chest.hasTreasure)
            {
                chest.gameObject.SetActive(false);

            }
            yield return new WaitForSecondsRealtime(1f);
        }
        content.gameObject.SetActive(false);
        openChest.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        openChest.SetActive(false);
        openChest2.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        openChest2.SetActive(false);
        openChest.SetActive(false);
        bonusPanel.SetActive(false);
        animate.startPause = true;
        if (Player.healthPoints <= 4)
        {
            Player.healthPoints += 1;
        }

    }

}
