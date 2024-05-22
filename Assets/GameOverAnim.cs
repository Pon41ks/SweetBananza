using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        EventManager.OnGameOver.AddListener(PlayGameOverAnimation);
    }
  
    private void PlayGameOverAnimation()
    {
        animator.SetTrigger("GameOver");
        Debug.Log("qwe");
    }
}
