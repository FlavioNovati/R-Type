using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Movement Movement;
    [Header("Shooting")]
    [SerializeField] List<ShootComponent> Shooting;

    private void Update()
    {
        //move
        Vector2 direction = Vector2.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        Movement.SetDirection(direction.normalized);
        //Shoot
        if(Input.GetButton("Shoot"))
        {
            for(int i = 0; i < Shooting.Count; i++)
                Shooting[i].Shoot();
        }
    }

    private void OnEnable()
    {
        GameManager.OnPlayerDeath += () => Movement.SetDirection(Vector2.zero);
        GameManager.OnPlayerDeath += () => this.enabled = false;
    }
}
