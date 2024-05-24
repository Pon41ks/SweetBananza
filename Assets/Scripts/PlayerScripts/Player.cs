using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Player : MonoBehaviour, IControllAble
{

    [Header("Preference")]
    [SerializeField] private GameObject Wheelpanel;
    private CharacterController character;
    public static Vector3 direction;
    private SpriteRenderer spriteRenderer;
    private AnimatorStateInfo currentAnimatorState;
    [SerializeField] private Sprite idle;
    [SerializeField] private Sprite takeDamage;
    [SerializeField] private Color colorRed;
    [SerializeField] private Color colorWhite;
    [SerializeField] private AudioSource hit;
    [SerializeField] private Image mask;
    private Animator animator;

    [Header("Properties")]
    [SerializeField] private float gravity = 9.81f * 2;
    [SerializeField] private float jumpForce = 10f;
    
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
        healthPoints = 4;
        mask.fillAmount = 1;
        isJumping = false;
        isSitting = false;
        isCanHitting = true;
        OffAnimation();
        character.height = 9.11f;
        spriteRenderer.color = colorWhite;

    }
    private void OffAnimation()
    {
        animator.Play("Idle");
        animator.Rebind();
        spriteRenderer.sprite = idle;

    }


    private void Update()
    {
        mask.fillAmount = (float)healthPoints / 4;
        
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
        if (!EventManager.isCantControl && isCanHitting)
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

    #region JumpAnimate

    public void ChangeCollider()
    {
        character.height = 5.78f;
    }

    public void ChangerCollider2()
    {
        character.height = 8.77f;
    }

    public void ChangerCollider3()
    {
        character.height = 9.11f;
    }

    #endregion
    private void PlayAnimation()
    {
        animator.speed = 1;
    }



    #region SlideAnimation

    public void OnDown()
    {
        character.height = 5.65f;
    }

    public void OnUP()
    {
        character.height = 9.11f;
        isSitting = false;
    }
    #endregion
    

    private void SLide()
    {
        isSitting = true;
        animator.SetTrigger("Slide");
    }




    /*
    private IEnumerator CanHitting()
    {
        isCanHitting = false;
        yield return new WaitForSeconds(1f);
        isCanHitting = true;

    }
    */
    private IEnumerator TakingHit()
    {
        if (!isSitting && !isJumping)
        {
            animator.SetTrigger("Hit");
            isCanHitting = false;
            character.height = 5.68f;
        }
        spriteRenderer.color = colorRed;

        yield return new WaitForSeconds(0.4f);
        
        spriteRenderer.color = colorWhite;
        character.height = 9.11f;
        yield return new WaitForSeconds(0.8f);
        isCanHitting = true;
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
                //StartCoroutine(CanHitting());
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
            EventManager.SendMultiplierChanged();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Wheel"))
        {
            EventManager.SetPlayerFrozen(true);
            EventManager.SendFruitIsColected();
            Destroy(other.gameObject);
            Wheelpanel.SetActive(true);
        }
    }
}
