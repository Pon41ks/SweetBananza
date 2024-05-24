using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager 
{
    public static int collectedFruits { get; private set; }
    public static bool isNewRecord  { get; private set; }
    public static bool isAnimationOver { get; private set; }
    public static bool isCantControl { get; private set; }
    public static bool isPause { get; private set; }
    public static bool isFrozen { get; private set; }
    public static bool isGameOver { get; private set; }
    public static float scoreMultiplier { get; private set; }



    //public static readonly UnityEvent OnCollectedFruit = new();
    // public static readonly UnityEvent OnClearfruitsCount = new();
    public static readonly UnityEvent OnRecordChanged = new UnityEvent();
    public static readonly UnityEvent OnAnimationEnd = new UnityEvent();
    public static readonly UnityEvent OnSpinStart = new UnityEvent();
    public static readonly UnityEvent OnSpinFinish = new UnityEvent();
    public static readonly UnityEvent OnPlayerIsFrozen = new UnityEvent();
    public static readonly UnityEvent OnPlayerUnFrozen = new UnityEvent();
    public static readonly UnityEvent OnContinueGame = new UnityEvent();
    public static readonly UnityEvent OnGameOver = new UnityEvent();
    public static readonly UnityEvent OnScoreMultiplierChanged = new UnityEvent();
    public static readonly UnityEvent OnWebViewOpened = new UnityEvent();

    static EventManager()
    {
        scoreMultiplier = 1.0f;
    }

    public static void SendWebViewOpened(WebViewObject webViewObject)
    {
        OnWebViewOpened.Invoke();
    }

    public static void SendMultiplierChanged()
    {
        scoreMultiplier += 0.2f;
        OnScoreMultiplierChanged.Invoke();

    }

    public static void ResetMultiplier()
    {
        scoreMultiplier = 1.0f;
    }
    public static void SendGameIsOver(bool overed)
    {
       isGameOver = overed;
        if (overed)
        {
            isGameOver = true;
            OnGameOver.Invoke();
        }
        
    }
    public static void SendContinueGame()
    {
        OnContinueGame.Invoke();
    }

    public static void SetPlayerFrozen(bool frozen)
    {
        isFrozen = frozen;
        if (isFrozen)
        {
            OnPlayerIsFrozen.Invoke();
        }
        else
        {
            OnPlayerUnFrozen.Invoke();
        }
    }


    public static void SetGamePaused()
    {
        if (isPause)
        {
            isPause = false;
        }
        else
        {
            isPause = true;
        }
        Debug.Log(isPause);
    }
   
    public static void SendFruitIsColected()
    {
        collectedFruits += 1;
        //OnCollectedFruit.Invoke();
        
    }

    public static void SendClearFruits()
    {
        collectedFruits = 0;
       // OnClearfruitsCount.Invoke();
    }

    public static void SendCantControl()
    {
        isCantControl = true;
    }

    public static void SendCanControl()
    {
        isCantControl = false;
    }
    public static void SendRecordChanged()
    {
        isNewRecord = true;
        OnRecordChanged.Invoke();
    }
    public static void SendAnimationEnded()
    {
        isNewRecord = false;
        isAnimationOver = true;
        OnAnimationEnd.Invoke();
    }

}
