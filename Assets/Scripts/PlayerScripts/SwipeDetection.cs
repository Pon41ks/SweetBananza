using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private InputAction position, press;

    [SerializeField] private float swipeResitance = 20f;

    public static SwipeDetection instance;
    public delegate void Swipe(Vector2 direction);
    public event Swipe SwipePerformed;
    

    private Vector2 initialPos;
    private Vector2 currentPos => position.ReadValue<Vector2>();

    private void Awake()
    {
        position.Enable();
        press.Enable();
        press.performed += _ => { initialPos = currentPos; };
        press.canceled += _ => DetectSwipe();
        instance = this;
    }

    private void DetectSwipe()
    {
        Vector2 delta = currentPos - initialPos;

        Player.direction = Vector2.zero;



        if (Mathf.Abs(delta.y) > swipeResitance)
        {

            Player.direction.y = Mathf.Clamp(delta.y, -1, 1);
        }
        if(Player.direction != Vector3.zero && SwipePerformed != null)
        {
            SwipePerformed(Player.direction);
        }
    }
   
}


