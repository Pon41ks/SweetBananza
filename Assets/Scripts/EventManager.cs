using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager 
{
    public static int collectedFruits { get; private set; }
    public static bool isNewRecord  { get; private set; }
    public static bool isAnimationOver { get; private set; }
    public static bool isSettingsOpen { get; private set; }



    //public static readonly UnityEvent OnCollectedFruit = new();
    // public static readonly UnityEvent OnClearfruitsCount = new();
    public static readonly UnityEvent OnRecordChanged = new();
    public static readonly UnityEvent OnAnimationEnd = new();

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
    

    public static void SendSettingsOpened()
    {
        isSettingsOpen = true;
    }

    public static void SendSettingsClosed()
    {
        isSettingsOpen = false;
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
