using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour, IControllAble
{

    [Header("Preference")]
    [SerializeField] private GameObject Wheelpanel;
    private CharacterController character;
    public static Vector3 direction;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite idle;
    [SerializeField] private Sprite takeDamage;
    [SerializeField] private Color colorRed;
    [SerializeField] private Color colorWhite;


    [Header("Properties")]
    [SerializeField] private float gravity = 9.81f * 2;
    [SerializeField] private float jumpForce = 10f;
    private Animator animator;
    public static bool isSitting;
    public static bool isJumping;
    public static bool isCanHitting = true;
    public static int healthPoints;



    private void Awake()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
       


    }

    private void OnEnable()
    {
        isJumping = false;
        isSitting = false;
        isCanHitting = true;

        character.height = 9.11f;


    }
    private void OffAnimation()
    {

        animator.Play("Idle");
        animator.Rebind();
        spriteRenderer.sprite = idle;
        


    }


    private void Update()
    {

        direction += gravity * Time.deltaTime * Vector3.down;
        character.Move(direction * Time.deltaTime);
        

    }

    public void Control()
    {
        if (!EventManager.isSettingsOpen)
        {
            if (SwipeDetection.direction == Vector2.up && !isJumping && !isSitting)
            {

                StartCoroutine(Jump());

            }
            else if (SwipeDetection.direction == Vector2.down && !isJumping && !isSitting)
            {


                StartCoroutine(Sliding());

            }

        }
        

    }

    private IEnumerator Jump()
    {

        if (character.isGrounded)
        {
            direction = Vector3.down;
            direction = Vector3.up * jumpForce;
            isJumping = true;
            animator.SetTrigger("Jump");
        }

        yield return new WaitForSeconds(1);
        isJumping = false;

    }

    private IEnumerator Sliding()
    {
        if (!isJumping)
        {
            isSitting = true;
            character.height = 5.65f;
            animator.SetTrigger("Slide");
        }

        yield return new WaitForSeconds(0.5f);
        character.height = 9.11f;
        isSitting = false;

    }


    private IEnumerator CanHitting()
    {
        
        isCanHitting = false;
        yield return new WaitForSeconds(1f);
        isCanHitting = true;

    }
    private IEnumerator TakingHit()
    {
        if (!isSitting && !isJumping)
        {
            animator.SetTrigger("Hit");
        }
        spriteRenderer.color = colorRed;
        character.height = 5.68f;
        yield return new WaitForSeconds(0.4f);
        spriteRenderer.color = colorWhite;
        character.height = 9.11f;
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
                
                if (healthPoints <= 0)
                {
                    OffAnimation();
                    GameManager.Instance.GameOver();
                }
                if (healthPoints >= 1)
                {
                    StartCoroutine(TakingHit());
                }
                
            }

        }
        if (other.CompareTag("Fruit"))
        {
            EventManager.SendFruitIsColected();
            Destroy(other.gameObject);
            Wheelpanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
