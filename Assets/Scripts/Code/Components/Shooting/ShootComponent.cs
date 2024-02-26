using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private Vector3 MuzzleOffset;
    [SerializeField] private Projectile ProjectileToShoot;
    [SerializeField] private float ShootDelay = 1.5f;

    private float ShootTime;
    private void Awake()
    {
        ShootTime = 0f;
    }

    public void Shoot()
    {
        if (ShootTime <= 0f)
        {
            StopAllCoroutines();
            ShootTime = ShootDelay;
            StartCoroutine(DecreaseTimer());
            Projectile bullet = Instantiate(ProjectileToShoot, transform.position + MuzzleOffset, Quaternion.identity);
            bullet.SetDirection(Vector3.right);
        }
    }

    IEnumerator DecreaseTimer()
    {
        while(ShootTime > 0f)
        {
            ShootTime -= Time.fixedDeltaTime;
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + MuzzleOffset, 0.1f);
    }
}
