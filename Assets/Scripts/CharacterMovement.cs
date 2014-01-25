using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3.0F;
    
    private Vector3 _moveDirection;
    
    void Awake()
    {
        _moveDirection = new Vector3();
    }

    public void Move(float horizontal, float vertical, float dt)
    {
        _moveDirection.x = horizontal;
        _moveDirection.y = 0f;
        _moveDirection.z = vertical;
        _moveDirection.Normalize();
        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection *= speed * dt;
        
        rigidbody.velocity = _moveDirection;
    }

}