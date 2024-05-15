using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRecord : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        EventManager.OnRecordChanged.AddListener(ShowNewRecord);
        animator = GetComponent<Animator>();
    }

    private void ShowNewRecord()
    {
        StartCoroutine(ShowAnimation());
    }

    private IEnumerator ShowAnimation()
    {
        animator.SetTrigger("NewRecord");
        yield return new WaitForSeconds(5);
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(1);
        EventManager.SendAnimationEnded();
    }

}
