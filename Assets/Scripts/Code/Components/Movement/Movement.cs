using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    [SerializeField] Vector2 MaxSpeed = Vector2.one;

    Rigidbody2D RigidBody;

    private void Awake()
    {
        RigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        //update velocity
        RigidBody.velocity = direction * Speed;
        //clamp the velocity
        float speedX = Mathf.Clamp(RigidBody.velocity.x, -MaxSpeed.x, MaxSpeed.x);
        float speedY = Mathf.Clamp(RigidBody.velocity.y, -MaxSpeed.y, MaxSpeed.y);
        RigidBody.velocity = new Vector2(speedX, speedY);
    }
}
