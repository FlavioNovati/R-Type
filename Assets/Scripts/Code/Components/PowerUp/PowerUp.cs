using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    SpriteRenderer SpriteRenderer;
    Collider2D Collider;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<Collider2D>();
    }

    public virtual void Trigger(Collider2D collision)
    {
        
    }

    private void Hide()
    {
        SpriteRenderer.enabled = false;
        Collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Trigger(collision);
        Hide();
    }
}
