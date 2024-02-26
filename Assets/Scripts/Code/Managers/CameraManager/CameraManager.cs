using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float Speed = 5f;

    private void FixedUpdate()
    {
        transform.position += Vector3.right * Speed * Time.fixedDeltaTime;
    }

    private void OnEnable()
    {
        
    }

}
