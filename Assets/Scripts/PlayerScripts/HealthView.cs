using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image[] healthPoints;
    [SerializeField] private int numOfHearts;
   


    private void Update()
    {
      
        numOfHearts = Player.healthPoints;
        for (int i = 0; i < healthPoints.Length; i++)
        {
            if (i < numOfHearts)
            {
                healthPoints[i].enabled = true;
            }
            else healthPoints[i].enabled = false;
        }
    }


}

