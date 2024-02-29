using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigExplosion : MonoBehaviour
{
    [SerializeField] Transform ExplosionPrefab;
    [SerializeField] float TimeBetweenExplosions;
    [SerializeField] float ExplosionRadius;
    [SerializeField] int ExplosionAmount = 5;

    WaitForSeconds WaitTime;
    private void Start()
    {
        WaitTime = new WaitForSeconds(TimeBetweenExplosions);
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        yield return null;
        for(int i = 0; i < ExplosionAmount; i++)
        {
            Vector3 explosionPos = transform.position;
            explosionPos.x += UnityEngine.Random.Range(-ExplosionRadius, ExplosionRadius);
            explosionPos.y += UnityEngine.Random.Range(-ExplosionRadius, ExplosionRadius);
            Instantiate(ExplosionPrefab, explosionPos, Quaternion.identity);
            yield return WaitTime;
        }
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector3.one * ExplosionRadius * 2);
    }
}
