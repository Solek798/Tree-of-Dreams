using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private CharacterController _controller;

    public float speed = 6.0f;
    public float jumpDuration = 8.0f;
    public float gravity = 20.0f;

    public float rotateSpeed = 5;

    private Vector3 _moveDirection = new Vector3(0, 0, 0);


    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var step = rotateSpeed * Time.deltaTime;
        if (_controller.isGrounded)
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            _moveDirection = cam.transform.TransformDirection(_moveDirection);


            _moveDirection.y = 0;
            Vector3 newDir = Vector3.MoveTowards(transform.forward, _moveDirection, step);
            transform.rotation = Quaternion.LookRotation(newDir);


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