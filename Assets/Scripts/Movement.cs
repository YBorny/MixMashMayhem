using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _slowDownMultiplier = 2f;
    [SerializeField] private float _rotationSpeed = 1f;

    [SerializeField] private float _dashSpeed = 2f;
    [SerializeField] private float _dashTime = .5f;

    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private LayerMask _groundLayer;

    private Vector2 _input;
    private CharacterController _controller;

    private bool _dash = false;
    private Coroutine _dashRoutine;

    private bool _jump = false;

    private bool _grounded = false;

    private float _currentMovementSpeed = 0;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Mathf.Abs(_input.y) > 0)
            _currentMovementSpeed = Mathf.Lerp(_currentMovementSpeed, _movementSpeed, Time.deltaTime);
        else
            _currentMovementSpeed = Mathf.Lerp(_currentMovementSpeed, 0, Time.deltaTime * _slowDownMultiplier);

        if (!_dash && Input.GetKeyDown(KeyCode.Space))
            _dash = true;

        CheckGrounded();
    }

    private void CheckGrounded()
    {
        var colls = Physics.OverlapSphere(transform.position + (Vector3.up * _controller.radius * .99f), _controller.radius, _groundLayer);
        _grounded = colls.Length > 0;
    }

    private void FixedUpdate()
    {
        Vector3 movement = transform.forward * _currentMovementSpeed * _input.y;
        transform.localEulerAngles = transform.localEulerAngles + (Vector3.up * _rotationSpeed * _input.x);

        if(!_grounded)
            movement += Vector3.down * _gravity;

        if (_dash && _dashRoutine == null)
            _dashRoutine = StartCoroutine(DashRoutine());

        _controller.Move(movement * Time.deltaTime);
    }

    IEnumerator DashRoutine()
    {
        float startTime = Time.time;
        _controller.enabled = false;

        float dash = _dashSpeed;

        while(Time.time < startTime + _dashTime)
        {
            dash = Mathf.Lerp(dash, 0, Time.deltaTime);
            transform.position += transform.forward * dash * Time.deltaTime;
            yield return null;
        }

        _controller.enabled = true;
        _dash = false;
        _dashRoutine = null;
    }
}
