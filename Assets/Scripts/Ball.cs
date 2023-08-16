using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed = 50f;
    [Range(0, .3f)][SerializeField] private float _movementSmoothing = 0.05f;

    private Vector2 _direction;
    private Vector2 _initialPosition;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _velocity = Vector2.zero;
    private Boundary _boundary;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.isKinematic = true;

        _direction = Vector2.one.normalized;
        _initialPosition = transform.position;

        _boundary = new Boundary();
    }

    private void FixedUpdate()
    {
        float move = _speed * 10f * Time.fixedDeltaTime;
        Vector2 targetVelocity = _direction * move;
        _rigidbody2D.velocity = Vector2.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);

        CheckIfOutOfUpperBounds();
        CheckIfPastPaddleBounds();
    }

    private void CheckIfOutOfUpperBounds()
    {
        bool isBallGoingAboveBorder = transform.position.y + transform.localScale.y / 1.25f > _boundary.Bounds.y && _direction.y > 0;
        bool isBallGoingBelowBorder = transform.position.y - transform.localScale.y / 1.25f < -_boundary.Bounds.y && _direction.y < 0;

        if (isBallGoingAboveBorder || isBallGoingBelowBorder)
        {
            _direction.y *= -1;
        }
    }

    private void CheckIfPastPaddleBounds()
    {
        bool isBallPastRightPaddle = transform.position.x + transform.localScale.x / 1.25f > _boundary.Bounds.x && _direction.x > 0;
        bool isBallPastLeftPaddle = transform.position.x - transform.localScale.x / 1.25f > _boundary.Bounds.x && _direction.x < 0;

        if(isBallPastRightPaddle)
        {
            transform.position = _initialPosition;

            _direction.y = GetRandomYDirection();
            _direction = new Vector2(1, _direction.y);
        }

        if (isBallPastLeftPaddle)
        {
            transform.position = _initialPosition;

            _direction.y = GetRandomYDirection();
            _direction = new Vector2(-1, _direction.y);
        }
    }

    private float GetRandomYDirection()
    {
        float directionY = 0;

        //prevent y direction from being equal to 0
        while(directionY == 0)
        {
           directionY = Mathf.Round(Random.Range(-1.0f, 1.0f));
        }

        return directionY;
    }
}
