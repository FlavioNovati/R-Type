using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float ProjectileDamage = 2f;
    [SerializeField] public float Damage { get; private set; }
    [SerializeField] private Transform VFX_OnDeath;
    [SerializeField] private SpriteRenderer ProjectileSprite;
    [SerializeField] private float Speed = 5f;

    private Rigidbody2D Rigidbody2D;

    private Vector2 Direction = Vector2.zero;

    private void Awake()
    {
        Damage = ProjectileDamage;
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 dir)
    {
        Direction = dir;
        if (Direction.x < 0)
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
