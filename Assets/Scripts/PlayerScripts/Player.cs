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
    private AnimatorStateInfo currentAnimatorState;
    private int currentAnimatorHash;
    private float currentAnimatorTime;
    [SerializeField] private Sprite idle;
    [SerializeField] private Sprite takeDamage;
    [SerializeField] private Color colorRed;
    [SerializeField] private Color colorWhite;
    [SerializeField] private AudioSource hit;


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
        EventManager.OnPlayerUnFrozen.AddListener(PlayAnimation);
        hit.volume = 1f;
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

        if (!EventManager.isFrozen)
        {
            
                direction += gravity * Time.deltaTime * Vector3.down;
                character.Move(direction * Time.deltaTime);
            
        }
        if (EventManager.isFrozen)
        {
            animator.speed = 0;
        }
    }

    public void Control()
    {
        if (!EventManager.isCantControl)
        {
            if (SwipeDetection.direction == Vector2.up && !isJumping && !isSitting)
            {

                StartCoroutine(Jump());


            }
            else if (SwipeDetection.direction == Vector2.down && !isJumping && !isSitting)
            {
                SLide();

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
    private void PlayAnimation()
    {
        animator.speed = 1;
    }

    private IEnumerator Sliding()
    {

        yield return new WaitForSeconds(0.3f);

        isSitting = false;

    }

    private void SLide()
    {
        isSitting = true;
        character.height = 5.65f;
        animator.SetTrigger("Slide");
    }

    public void OnAnimationFinished()
    {
        StartCoroutine(Sliding());
        character.height = 9.11f;
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
            character.height = 5.68f;
        }
        spriteRenderer.color = colorRed;

        yield return new WaitForSeconds(0.4f);
        spriteRenderer.color = colorWhite;
        character.height = 9.11f;
    }

    private IEnumerator FreezeAnimationAndMovement()
    {


        
        Vector3 originalVelocity = direction;
        direction = Vector3.zero;

        while (EventManager.isFrozen)
        {
            yield return null;
        }

        // Возобновляем анимацию с сохраненного состояния и времени
        animator.Play(currentAnimatorHash, 0, currentAnimatorTime);
        animator.speed = 1;
        direction = originalVelocity;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (isCanHitting)
            {
                hit.Play();
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
            EventManager.SendPlayerFrozen();
            //StartCoroutine(FreezeAnimationAndMovement());
            
            
            //OffAnimation();
            EventManager.SendFruitIsColected();
            Destroy(other.gameObject);
            Wheelpanel.SetActive(true);

        }
    }
}
