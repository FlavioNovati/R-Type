using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    [SerializeField] float FinalPosition = 290f;
    [SerializeField] Color GizsmoColor = Color.yellow;
    private void FixedUpdate()
    {
        if(transform.position.x < FinalPosition)
            transform.position += Vector3.right * Speed * Time.fixedDeltaTime;
    }

    private void OnEnable()
    {
        GameManager.OnPlayerDeath += () => this.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizsmoColor;
        Gizmos.DrawCube(transform.position + (Vector3.right * FinalPosition), Vector3.one);
    }
}
