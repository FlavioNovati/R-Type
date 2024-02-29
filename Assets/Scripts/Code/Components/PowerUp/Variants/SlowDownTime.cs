using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownTime : PowerUp
{
    [SerializeField] float NewTimeScale = 0.5f;
    [SerializeField] float EffectDuration = 4f;
    [SerializeField] float Speed = 5f;

    public override void Trigger(Collider2D collision)
    {
        StartCoroutine(ApplyEffect());
    }

    private IEnumerator ApplyEffect()
    {
        while(Time.timeScale > NewTimeScale)
        {
            Time.timeScale -= Time.fixedDeltaTime * Speed;
            yield return null;
        }

        yield return new WaitForSeconds(EffectDuration);

        while (Time.timeScale < 1)
        {
            Time.timeScale += Time.fixedDeltaTime * Speed;
            yield return null;
        }
        Time.timeScale = 1f;
    }
}
