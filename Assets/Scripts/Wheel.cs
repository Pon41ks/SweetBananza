using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Wheel : MonoBehaviour
{

    private float timeInterval;
    private int randomValue;
    private bool coroutineAllowed;
    private int finalAngle;
    private bool spinStart;
    public bool isSpinnigFinish;

    [SerializeField] private GameObject wheelPanel;
    [SerializeField] private GameObject spinButton;
    [SerializeField] private Animate animate;
    
    private void OnEnable()
    {
        spinButton.SetActive(true);
        coroutineAllowed = true;
        isSpinnigFinish = false;
    }

    public void Spin()
    {
        spinStart = true;
        spinButton.SetActive(false);
    }

    private void Update()
    {
        
        if (spinStart)
        {
            StartCoroutine(Spining());
        }

    }


    private IEnumerator Spining()
    {
   
        coroutineAllowed = false;
        randomValue = Random.Range(20, 30);
        timeInterval = 0.1f;
        

        for (int i = 0; i < randomValue; i++)
        {
            transform.Rotate(0, 0, 22.5f);
            if (i > Mathf.RoundToInt(randomValue * 0.5f))
            {
                timeInterval = 0.2f;
            }
            if (i > Mathf.RoundToInt(randomValue * 0.85f))
            {
                timeInterval = 0.4f;
            }
            spinStart = false;
    
            yield return new WaitForSecondsRealtime(timeInterval);
         
        }
        if (Mathf.RoundToInt(transform.eulerAngles.z) % 45 != 0 )
        {
            transform.Rotate(0, 0, 22.5f);
        }

        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);

        switch (finalAngle)
        {
            case 0:
                if (Player.healthPoints < 4)
                {
                    Player.healthPoints += 1;
                }
                Debug.Log("you win");
                break;
            case 180:
                if (Player.healthPoints < 4)
                {
                    Player.healthPoints += 1;
                }
                Debug.Log("you win");
                break;
            case 90:
                if (Player.healthPoints < 4)
                {
                    Player.healthPoints += 1;
                }
                Debug.Log("you win");
                break;




        }
        
        
        isSpinnigFinish = true;
        coroutineAllowed = true;
        animate.startPause = true;
        wheelPanel.SetActive(false);
        
    }

}

