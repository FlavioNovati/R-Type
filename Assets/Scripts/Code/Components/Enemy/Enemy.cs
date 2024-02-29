using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Action EnemyDestroyed;

    [Header("Enemy")]
    [SerializeField] float HP = 10f;
    [SerializeField] int ScoreOnDefeat = 5;
    [SerializeField] float InvulnerabilityTime = 2f;
    [Header("Movement")]
    [SerializeField] private Movement EnemyMovement;
    [SerializeField] private float HeightChangeFrequency = 1.5f;
    [Header("Shooting")]
    [SerializeField] private Vector3 MuzzleOffset;
    [SerializeField] private Projectile ProjectileToShoot;
    [SerializeField] private float ShootDelay = 1.5f;
    [Header("On Death")]
    [SerializeField] private Transform ObjectOnDeath;
    [SerializeField] private float DropProb = 0.2f;
    [SerializeField] private List<Transform> DropOnDeath;
    [SerializeField] private bool DestroyOnDeath = true;
    
    private float ChangeHeightTime;
    private float ShootTime;
    private Vector3 Direction = Vector2.left;
    private Collider2D Collider;
    private Rigidbody2D Rigidbody;

    private bool Dead = false;
    private bool Invulnerable = true;

    private void Awake()
    {
        ChangeHeightTime = HeightChangeFrequency;
        ShootTime = ShootDelay;
        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(Invulnerability());
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
        Dead = true;
        Collider.enabled = false;
        Instantiate(ObjectOnDeath, transform.position, Quaternion.identity);
        GameManager.OnIncrementScore?.Invoke(ScoreOnDefeat);
        if(DropOnDeath.Count > 0)
            if (UnityEngine.Random.value > DropProb)
            {
                Instantiate(DropOnDeath[UnityEngine.Random.Range(0, DropOnDeath.Count)], transform.position, Quaternion.identity);
            }
        EnemyDestroyed();
        if(DestroyOnDeath)
            Destroy(gameObject);
    }

    private IEnumerator Invulnerability()
    {
        Invulnerable = true;
        this.Rigidbody.isKinematic = true;
        yield return new WaitForSeconds(InvulnerabilityTime);
        Invulnerable = false;
        this.Rigidbody.isKinematic = false;
    }

    private void OnEnable()
    {
        EnemyDestroyed += () => { };
    }

    private void OnDisable()
    {
        EnemyDestroyed -= EnemyDestroyed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if(projectile != null)
        {
            if(!Invulnerable)
                HP -= projectile.Damage;
            if(HP < 0 && !Dead)
                Die();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + MuzzleOffset, 0.1f);
    }
}
