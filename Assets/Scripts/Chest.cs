using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField] private GameObject bonusPanel;

    public static bool isChoose;

    public  bool hasTreasure;
    private Animate animate;

    private void OnEnable()
    {
        animate = FindAnyObjectByType<Animate>();
    }

    public void OpenСhest()
    {
        
        if (hasTreasure)
        {
            isChoose = true;
            if (Player.healthPoints <= 4)
            {
                Player.healthPoints += 1;
            }
            
            Debug.Log("success");
        }
        else
        {
            animate.startPause = true;
            bonusPanel.SetActive(false);
            
        }

    }



}
