using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{

    [Header("Preference")]
    [SerializeField] private Sprite[] jumpSprites;
    [SerializeField] private Sprite[] sitDownSprites;
    [SerializeField] private Sprite[] hits;
    [SerializeField] private Sprite runSprites;
     private SpriteRenderer spriteRenderer;
    

    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void OnEnable()
    {
        StartCoroutine(Animate(jumpSprites, sitDownSprites, hits));
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    
    private IEnumerator Animate(Sprite[] jumpSprites, Sprite[] sitDown, Sprite[] hits)
    {
       
        for (float i = 0; i < 5; i += Time.deltaTime  )
        {
       
            frame++;
            if (Player.isJumping)
            {
                if (frame >= jumpSprites.Length)
                {
                    frame = 0;
                }

                if (frame >= 0 && frame < jumpSprites.Length)
                {
                    spriteRenderer.sprite = jumpSprites[frame];
                  
                }
             
            }
            if (Player.isSitting)
            {
                if (frame >= sitDown.Length)
                {
                    frame = 0;
                }
                if (frame >= 0 && frame < sitDown.Length)
                {
                    spriteRenderer.sprite = sitDown[frame];

                }
            }
            /*
            else if (!Player.isCanHitting)
            {
                if (frame >= hits.Length)
                {
                    frame = 0;
                }
                if (frame >= 0 && frame < hits.Length)
                {
                    spriteRenderer.sprite = hits[frame];

                }
            }
            */
            else if (!Player.isJumping)
            {
                spriteRenderer.sprite = runSprites;
            }
            

            yield return new WaitForSeconds( 1.1f /GameManager.Instance.gameSpeed);
      
            
        }

    }

}
