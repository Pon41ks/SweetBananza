using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private float leftEdge;

    private void Update()
    {

        transform.position += Vector3.left *  GameManager.Instance.GameSpeed / 3f * Time.deltaTime;

        if (transform.position.x < leftEdge || EventManager.isFrozen)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }
}

