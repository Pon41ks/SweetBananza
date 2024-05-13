using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IControllAble
{

    [Header("Preference")]
    [SerializeField] private GameObject Wheelpanel;
    private CharacterController character;
    public static Vector3 direction;

    [Header("Properties")]
    [SerializeField] private float gravity = 9.81f * 2;
    [SerializeField] private float jumpForce = 10f;
    private Animator animator;
    public static bool isSitting;
    public static bool isJumping = false;
    public static bool isCanHitting = true;
    public static int healthPoints;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
    }

    private void OnEnable()
    {
        isJumping = false;
        isSitting = false;
        isCanHitting = true;
    }


    private void Update()
    {
        direction += gravity * Time.deltaTime * Vector3.down;
        character.Move(direction * Time.deltaTime);
        
    }

    public void Control()
    {

        if (direction == Vector3.up)
        {

            StartCoroutine(Jump());
        }
        else if (direction == Vector3.down)
        {
            StartCoroutine(Sliding());

        }

    }

    private IEnumerator Jump()
    {
        if (character.isGrounded && !isSitting)
        {
            direction = Vector3.down;
            direction = Vector3.up * jumpForce;
            isJumping = true;
            animator.SetTrigger("Jump");
        }
        
        yield return new WaitForSeconds(0.7f);
        isJumping = false;
    }

    private IEnumerator Sliding()
    {
        if (!isSitting && !isJumping)
        {
            character.height = 5.65f;
            isSitting = true;
            animator.SetTrigger("Slide");
        }

        isSitting = false;
        yield return new WaitForSeconds(0.5f);
        
        character.height = 9.11f;
       
    }

    
    private IEnumerator CanHitting()
    {

        isCanHitting = false;
        yield return new WaitForSeconds(3);
        isCanHitting = true;
      
    }
    


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (isCanHitting)
            {
                var target = GetComponent<IDamageable>();
                target?.TakeHit();
                healthPoints -= 1;
                StartCoroutine(CanHitting());
                GameManager.Instance.TakingHit();
                //StartCoroutine(CanHitting());
                if (healthPoints <= 0)
                {
                    GameManager.Instance.GameOver();
                }
            }
          
        }
        if (other.CompareTag("Fruit") )
        {
            Destroy(other.gameObject);
                Wheelpanel.SetActive(true);
                Time.timeScale = 0f;
        }
    }
}
