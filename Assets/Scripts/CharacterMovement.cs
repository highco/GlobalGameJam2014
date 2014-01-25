using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    public float speed = 3.0F;
    public float rotateSpeed = 3.0F;
    
    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private int _index;
    
    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _moveDirection = new Vector3();
        _index = GetComponent<Player>().playerIndex;
    }
    
    void Update()
    {
        _moveDirection.x = Input.GetAxis("Horizontal" + _index);
        _moveDirection.y = 0f;
        _moveDirection.z = Input.GetAxis("Vertical" + _index);
        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection *= speed;
        _characterController.Move(_moveDirection * Time.deltaTime);
    }
}