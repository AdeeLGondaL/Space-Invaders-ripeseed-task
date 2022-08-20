using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public Sprite[] SpriteAnimations;
    public float AnimationTime = 1.0f;

    public System.Action Killed;

    private SpriteRenderer spriteRenderer;

    private int currentSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        InvokeRepeating(nameof(SpriteAnimation), AnimationTime, AnimationTime);
    }

    private void SpriteAnimation()
    {
        currentSprite++;

        if(currentSprite >= SpriteAnimations.Length)
        {
            currentSprite = 0;
        }

        spriteRenderer.sprite = SpriteAnimations[currentSprite];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            this.Killed.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
