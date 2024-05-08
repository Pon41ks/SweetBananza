using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterInputController : MonoBehaviour
{
    private IControllAble controllAble;
    private void Awake()
    {
        controllAble = GetComponent<IControllAble>();

    }

    private void Update()
    {
        SwipeDetection.instance.SwipePerformed += context => controllAble.Control();
    }
}
