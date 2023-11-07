using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornObject : MonoBehaviour
{
    private SpriteRenderer _thornObjectRenderer;
    private BoxCollider2D _thornCollider;
    private bool isVisible = true;
    [SerializeField] private float blinkInterval = 1.0f; 

    void Start()
    {
        _thornObjectRenderer = GetComponent <SpriteRenderer>();
        _thornCollider = GetComponent <BoxCollider2D>();

        StartCoroutine(BlinkObstacle());
    }

    IEnumerator BlinkObstacle()
    {
        while (true)
        {
            isVisible = !isVisible;
            _thornObjectRenderer.enabled = isVisible;
            _thornCollider.enabled = isVisible;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
