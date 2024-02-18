using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Movement EnemyMovement;
    [SerializeField] private float HeightChangeFrequency = 1.5f;
    //OnDeath
    [SerializeField] private Transform ObjectOnDeath;

    private Vector3 Direction = Vector2.left;
    
    private float ChangeHeightTime;

    private void Awake()
    {
        ChangeHeightTime = HeightChangeFrequency;
    }

    private void FixedUpdate()
    {
        //Decrease direction change timer
        ChangeHeightTime -= Time.fixedDeltaTime;
        //move up or down
        if(ChangeHeightTime <= 0f)
        {
            //stop direction
            EnemyMovement.SetDirection(Vector2.zero);
            //reset Direction
            Direction = Vector2.left;
            //get new direction
            float heightChange = UnityEngine.Random.Range(-1f,1f);
            Direction += heightChange * Vector3.up;
            //Reset Timer
            ChangeHeightTime = HeightChangeFrequency;
        }


        //move to the left
        EnemyMovement.SetDirection(Direction);
    }

    private void Die()
    {
        Instantiate(ObjectOnDeath, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }
}
