
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp : PowerUp
{
    [SerializeField] float HealHp = 5f;

    public override void Trigger(Collider2D collision)
    {
        PlayerManager.OnHealthChanged?.Invoke(HealHp);
    }
}
