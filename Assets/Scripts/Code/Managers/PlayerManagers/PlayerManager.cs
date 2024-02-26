using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static Action<float> OnHealthChanged;

    [SerializeField] private Transform DeathVFX;
    
    private float HP = 10f;


    private void Start()
    {
        OnHealthChanged(HP);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            HP -= projectile.Damage;
            OnHealthChanged(HP);
            if(HP <= 0)
            {
                GameManager.OnPlayerDeath?.Invoke();
                Instantiate(DeathVFX, transform.position, transform.rotation);
            }
        }
    }

    private void OnDisable()
    {
        OnHealthChanged -= OnHealthChanged;
    }
}
