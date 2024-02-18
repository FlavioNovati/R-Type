using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Movement EnemyMovement;
    [SerializeField] private float HeightChangeFrequency = 1.5f;
    //OnDeath
    [Header("On Death")]
    [SerializeField] private Transform ObjectOnDeath;
    [Header("Shooting")]
    [SerializeField] private Vector3 MuzzleOffset;
    [SerializeField] private Projectile ProjectileToShoot;
    [SerializeField] private float ShootDelay = 1.5f;

    private Vector3 Direction = Vector2.left;
    
    private float ChangeHeightTime;
    private float ShootTime;

    private void Awake()
    {
        ChangeHeightTime = HeightChangeFrequency;
        ShootTime = ShootDelay;
    }

    private void FixedUpdate()
    {

        //move up or down
        if (ChangeHeightTime <= 0f)
        {
            ChangeHeightTime = HeightChangeFrequency;
            ChangeDirection();
        }
        //move to the left
        EnemyMovement.SetDirection(Direction);

        //Shoot
        if(ShootTime <= 0f)
        {
            ShootTime = ShootDelay;
            Shoot();
        }

        DecreaseTimers();
    }

    private void ChangeDirection()
    {
        //stop direction
        EnemyMovement.SetDirection(Vector2.zero);
        //reset Direction
        Direction = Vector2.left;
        //get new direction
        float heightChange = UnityEngine.Random.Range(-1f, 1f);
        Direction += heightChange * Vector3.up;
        //Reset Timer
        ChangeHeightTime = HeightChangeFrequency;
    }


    private void DecreaseTimers()
    {
        //Decrease direction change timer
        ChangeHeightTime -= Time.fixedDeltaTime;
        ShootTime -= Time.fixedDeltaTime;
    }

    private void Shoot()
    {
        Projectile bullet = Instantiate(ProjectileToShoot, transform.position + MuzzleOffset, Quaternion.identity);
        bullet.SetDirection(Vector3.left);
    }

    private void Die()
    {
        Instantiate(ObjectOnDeath, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + MuzzleOffset, 0.1f);
    }
}
