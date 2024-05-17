using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusGame : MonoBehaviour
{
    [SerializeField] private Chest[] chests;

    [SerializeField] private GameObject content;
    [SerializeField] private GameObject bonusPanel;
    [SerializeField] private GameObject bonusText;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject effect;
    [SerializeField] private Image[] chestss;


    public bool isCorrectChest;

    private void OnEnable()
    {
        foreach (var chest in chestss)
        {
            chest.enabled = true;
        }
        bonusText.SetActive(true);
        content.SetActive(true);
        EventManager.SendGamePaused();
        spawner.SetActive(false);
        EventManager.SendPlayerFrozen();
        animator.SetTrigger("ChestOpened");
        EventManager.SendCantControl();
        int index = Random.Range(0, 4);
        isCorrectChest = false;
        chests[index].hasTreasure = true;


        content.SetActive(true);
        foreach (var chest in chests)
        {
            chest.gameObject.SetActive(true);
        }

    }

    private void Update()
    {
        effect.transform.Rotate(0, 0, 0.1f);
        if (Chest.isChoose)
        {

            Chest.isChoose = false;
            isCorrectChest = true;
            foreach (var chest in chests)
            {
                if (chest.hasTreasure)
                {
                    animator.SetTrigger("1");
                }


            }


        }
        else if (Chest.incorrect)
        {
            animator.SetTrigger("Lose");
        }
       
        if (!EventManager.isPause)
        {
            spawner.SetActive(true);
        }
    }

    public void CloseBonus()
    {
        foreach (var chest in chests)
        {
            chest.hasTreasure = false;
        }

        animator.SetTrigger("2");
        bonusPanel.SetActive(false);
        EventManager.SendContinueGame();
    }
    public void OffImage()
    {
        foreach (var chest in chestss)
        {
            chest.enabled = false;
        }
    }

    /*
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
    */

}
