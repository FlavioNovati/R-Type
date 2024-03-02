using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public delegate void BossDefeated();
    public static BossDefeated OnBossDefeated;
    Enemy EnemyComponent;
    private bool Dead = false;

    private void Awake()
    {
        EnemyComponent = GetComponent<Enemy>();;
    }

    private IEnumerator BossKilled()
    {
        if (!Dead)
        {
            Dead = true;
            EnemyComponent.enabled = false;
            StartCoroutine(TriggerEnd());
        }
        yield return null;
    }

    private IEnumerator TriggerEnd()
    {
        yield return new WaitForSeconds(3f);
        OnBossDefeated();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyComponent.enabled = true;
        EnemyComponent.EnemyDestroyed += () => StartCoroutine(BossKilled());
        EnemyComponent.InitialInvulnerability();
    }
}
