using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3.0F;
    
    private Vector3 _moveDirection;
    private int _index;
    
    void Awake()
    {
        _moveDirection = new Vector3();
        _index = GetComponent<Player>().playerIndex;
    }

    public void Move(float horizontal, float vertical)
    {
        _moveDirection.x = horizontal;
        _moveDirection.y = 0f;
        _moveDirection.z = vertical;
        _moveDirection.Normalize();
        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection *= speed * Time.deltaTime;
        
        rigidbody.velocity = _moveDirection;
    }

}