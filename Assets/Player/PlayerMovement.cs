using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform lookAt = null;
    [SerializeField] private Camera cam;
    
    private CharacterController _controller;
    
    public float speed = 6.0f;
    public float jumpDuration = 8.0f;
    public float gravity = 20.0f;

    private Vector3 _moveDirection = new Vector3(0,0,0);
    
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_controller.isGrounded)
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

            var lookAtPoint = transform.position + _moveDirection;
            _moveDirection = cam.transform.TransformDirection(_moveDirection);
            
            
            
            
            transform.LookAt(lookAtPoint);
            
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