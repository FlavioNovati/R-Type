using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Movement MovementComponent;
    [SerializeField] public float Damage { get; private set; } = 1f;
    [SerializeField] private Transform VFX_OnDeath;
    [SerializeField] private SpriteRenderer ProjectileSprite;

    private Vector2 Direction = Vector2.zero;

    private void FixedUpdate()
    {
        MovementComponent.SetDirection(Direction);
    }

    public void SetDirection(Vector2 dir)
    {
        Direction = dir;
        if(Direction.x < 0)
        {
            ProjectileSprite.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(VFX_OnDeath, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
