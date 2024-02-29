using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPoweUp : PowerUp
{
    [SerializeField] private Vector2 MaxSpeedlimitMultiplier = Vector2.one * 2;
    [SerializeField] private float MaxSpeedMultiplier = 2f;
    [SerializeField] private float EffectDuration = 4f;

    Movement movement;

    public override void Trigger(Collider2D collision)
    {
        movement = collision.gameObject.GetComponent<Movement>();
        if(!movement)
            StartCoroutine(ApplyEffect());
    }

    IEnumerator ApplyEffect()
    {
        Vector2 oldMaxSpeed = movement.MaxSpeed;
        float oldMaxSpeedMultiplier = movement.Speed;

        movement.MaxSpeed *= MaxSpeedlimitMultiplier;
        movement.Speed *= MaxSpeedMultiplier;
        yield return new WaitForSeconds(EffectDuration);
        movement.MaxSpeed = oldMaxSpeed;
        movement.Speed = oldMaxSpeedMultiplier;
    }
}
