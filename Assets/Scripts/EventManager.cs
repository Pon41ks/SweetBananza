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



    //public static readonly UnityEvent OnCollectedFruit = new();
    // public static readonly UnityEvent OnClearfruitsCount = new();
    public static readonly UnityEvent OnRecordChanged = new();
    public static readonly UnityEvent OnAnimationEnd = new();
    public static readonly UnityEvent OnSpinStart = new();
    public static readonly UnityEvent OnSpinFinish = new();
    public static readonly UnityEvent OnPlayerIsFrozen = new();
    public static readonly UnityEvent OnPlayerUnFrozen = new();
    public static readonly UnityEvent OnContinueGame = new();


    public static void SendContinueGame()
    {
        OnContinueGame.Invoke();
    }

    public static void SendPlayerFrozen()
    {
        isFrozen = true;
        OnPlayerIsFrozen.Invoke();
    }

    public static void SendPlayerUnFrozen()
    {
        isFrozen = false;
        OnPlayerUnFrozen.Invoke();
    }
    public static void SendGamePaused()
    {
        isPause = true;
        OnSpinStart.Invoke();
    }


    public static void SendGameUnPaused()
    {
        isPause = false;

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
