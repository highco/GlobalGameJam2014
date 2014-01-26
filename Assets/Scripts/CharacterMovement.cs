using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3.0F;
    
    private Vector3 _moveDirection;
    private bool _controlsDisabled;

    void Awake()
    {
        _moveDirection = new Vector3();
    }

    public void Move(float horizontal, float vertical, float dt)
    {
        if (_controlsDisabled)
            return;

        if (Mathf.Abs(horizontal) > 0.1 || Mathf.Abs(vertical) > 0.1)
        {
            _moveDirection.x = horizontal;
            _moveDirection.y = 0f;
            _moveDirection.z = vertical;
            _moveDirection.Normalize();
            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection *= speed * dt;
        }
        else
            _moveDirection = Vector3.zero;

        rigidbody.velocity = _moveDirection;
    }

    public void Dash(float time)
    {
        rigidbody.velocity = rigidbody.velocity * 3f;
        _controlsDisabled = true;
        StartCoroutine(EnableControlsAfter(time));
    }

    IEnumerator EnableControlsAfter(float time)
    {
        yield return new WaitForSeconds(time);
        _controlsDisabled = false;
    }
}