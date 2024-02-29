using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static Action<float> OnHealthChanged;

    [SerializeField] private Transform DeathVFX;
    [SerializeField] private float PlayerHp = 25f;
    public static float HP = 25f;

    private float MaxHealth;

    private void Awake()
    {
        HP = PlayerHp;
        MaxHealth = HP;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            OnHealthChanged(-projectile.Damage);
        }
    }

    private void UpdateHp(float value)
    {
        HP += value;

        if (HP > MaxHealth)
            HP = MaxHealth;

        if (HP <= 0)
        {
            GameManager.OnPlayerDeath?.Invoke();
            Instantiate(DeathVFX, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        OnHealthChanged += UpdateHp;
    }

    private void OnDisable()
    {
        OnHealthChanged -= OnHealthChanged;
    }
}
