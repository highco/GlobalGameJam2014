using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3.0F;
    public float dashSpeedFactor = 2f;

    private Vector3 _moveDirection;
    private bool _controlsDisabled;

    private bool _dashing;

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
            if (_dashing)
                _moveDirection *= dashSpeedFactor;
        }
        else
            _moveDirection = Vector3.zero;

        rigidbody.velocity = _moveDirection;
    }

    public void SetDash(bool dash)
    {
        _dashing = dash;
    }

    IEnumerator EnableControlsAfter(float time)
    {
        yield return new WaitForSeconds(time);
        _controlsDisabled = false;
    }
}