using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerInput
{
    Vertical,
    AltVertical
}

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PaddleMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _speed = 5f;
    [Range(0,.3f)][SerializeField] private float _movementSmoothing = 0.05f;
    [SerializeField] private float _screenOffset = .5f;

    private string _input;
    private float _verticalMovement;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _velocity = Vector2.zero;
    private Boundary _boundary;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.isKinematic = true;

        _input = _playerInput.ToString();
        _boundary = new Boundary();
    }

    private void Update()
    {
        _verticalMovement = Input.GetAxisRaw(_input) * _speed;
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(_rigidbody2D.velocity.x, _verticalMovement * 10f * Time.fixedDeltaTime);
        CheckIfOutOfBounds(ref targetVelocity);
        _rigidbody2D.velocity = Vector2.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);
    }

    private void CheckIfOutOfBounds(ref Vector2 targetVelocity)
    {
        bool isPaddleGoingAboveBorder = transform.position.y + _screenOffset > _boundary.Bounds.y && _verticalMovement > 0;
        bool isPaddleGoingBelowBorder = transform.position.y - _screenOffset < -_boundary.Bounds.y && _verticalMovement < 0;

        if (isPaddleGoingAboveBorder || isPaddleGoingBelowBorder)
        {
            targetVelocity = Vector2.zero;
        }
    }
}
