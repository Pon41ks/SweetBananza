using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField] private GameObject bonusPanel;

    private BonusGame bg;

    public static bool isChoose;

    public  bool hasTreasure;
    private Animate animate;

    private void OnEnable()
    {
        animate = FindAnyObjectByType<Animate>();
        bg = FindAnyObjectByType<BonusGame>();
    }

    public void OpenСhest()
    {
        
        if (hasTreasure)
        {
            isChoose = true;

            
            Debug.Log("success");
        }
        else if(!hasTreasure && !bg.isCorrectChest)
        {

            animate.startPause = true;
            bonusPanel.SetActive(false);
            
        }

    }



}
