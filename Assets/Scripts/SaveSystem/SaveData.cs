using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public  class SaveData
{
    private static SaveData _current;
    public static SaveData Current
    {
        get => _current ??= new SaveData
        {
            highScore = 0,
        };
        set
        {
            if (value != null)
            {
                _current = value;
            }
        }
    }

    public float highScore;
}
