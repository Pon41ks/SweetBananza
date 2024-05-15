using UnityEngine;
using UnityEngine.InputSystem;

public class HardwareButtonHandler : MonoBehaviour
{
    [SerializeField] private InputAction backButtonAction;
    

    public static HardwareButtonHandler instance;
    public delegate void ButtonPress();
    public event ButtonPress BackButtonPressed;


    private void Awake()
    {
        backButtonAction.Enable();


        backButtonAction.performed += _ => { BackButtonDetected(); };


        instance = this;
    }

    private void OnEnable()
    {
        backButtonAction.Enable();

    }

    private void OnDisable()
    {
        backButtonAction.Disable();

    }

    private void BackButtonDetected()
    {
        Debug.Log("Back button pressed");
        BackButtonPressed?.Invoke();
        // Действия при нажатии кнопки "Назад"
        Application.Quit();
    }


}
