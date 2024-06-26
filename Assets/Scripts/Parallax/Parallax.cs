using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField] private Vector2 direction;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (!EventManager.isPause)
        {
            float speed = GameManager.Instance.GameSpeed / transform.localScale.x;
            meshRenderer.material.mainTextureOffset += direction * speed * Time.deltaTime;
        }
        
    }
}
