using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    
    public float speed = 6.0f;
    public float jumpDuration = 8.0f;
    public float gravity = 20.0f;

    private Vector3 _moveDirection = Vector3.zero;
    
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_controller.isGrounded)
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            _moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                _moveDirection.y = jumpDuration;
            }
        }
        _moveDirection.y -= gravity * Time.deltaTime;

        _controller.Move(_moveDirection * Time.deltaTime);
    }
}