using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private CharacterController _controller;

    public float speed = 6.0f;
    private float _normalSpeed = 0.0f;
    public float runSpeed = 12.0f;
    public float jumpDuration = 8.0f;
    public float gravity = 20.0f;
    public ParticleSystem playerTrail;

    public float rotateSpeed = 5;

    private Vector3 _moveDirection = new Vector3(0, 0, 0);


    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _normalSpeed = speed;
    }
//strg+r  + m

    private void Update()
    {
        CheckIfRunning();
        MovePlayer();
        if (IsMoving())
        {
            playerTrail.Emit(1);
        }
        else
        {
            playerTrail.Stop();
        }
    }

    private void MovePlayer()
    {
        var step = rotateSpeed * Time.deltaTime;

        if (_controller.isGrounded)
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * speed;

            _moveDirection = cam.transform.TransformDirection(_moveDirection);
            _moveDirection.y = 0;

            var newDir = Vector3.MoveTowards(transform.forward, _moveDirection, step);
            transform.rotation = Quaternion.LookRotation(newDir);

            //_moveDirection *= speed;
            Jump();
        }

        _moveDirection.y -= gravity * Time.deltaTime;
        _controller.Move(_moveDirection * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            _moveDirection.y = jumpDuration;
        }
    }


    private void CheckIfRunning()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }


        else if (Input.GetKeyUp((KeyCode.LeftShift)))
        {
            speed = _normalSpeed;
        }
    }

    private bool IsMoving()
    {
        var x = _moveDirection.x;
        var z = _moveDirection.z;

        return x >= 1 || z >= 1 || x <= -0.5 || z <= -0.5;
    }
}