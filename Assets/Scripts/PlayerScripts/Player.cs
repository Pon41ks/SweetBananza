using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Preference")]
    [SerializeField] private GameObject jumpBatton;
    [SerializeField] private GameObject jumpButton;
    private CharacterController character;
    private Vector3 direction;

    [Header("Properties")]
    [SerializeField] private float gravity = 9.81f * 2;
    [SerializeField] private float jumpForce = 8f;
    
    private  void Awake()
    {
        character = GetComponent<CharacterController>();
    }


    private void OnEnable()
    {
        direction = Vector3.zero;
    }
    private void Update()
    {
        direction += gravity * Time.deltaTime * Vector3.down;
        character.Move(direction * Time.deltaTime);
    }

    public void Jump()
    {
        if (character.isGrounded)
        {
            direction = Vector3.down;
            direction = Vector3.up * jumpForce;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
