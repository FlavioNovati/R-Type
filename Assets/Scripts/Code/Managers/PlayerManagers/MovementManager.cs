using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] Movement movement;
    
    private void Update()
    {
        Vector2 direction = Vector2.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        movement.SetDirection(direction.normalized);
    }
}
