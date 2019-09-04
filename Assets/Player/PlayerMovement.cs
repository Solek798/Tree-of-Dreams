using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera cam = null;
    [SerializeField] private AudioSource audioPlayer = null;

    private CharacterController _controller;

    public float speed = 6.0f;
    private float _normalSpeed = 0.0f;
    public float runSpeed = 12.0f;
    public float jumpDuration = 8.0f;
    public float gravity = 20.0f;
    //public ParticleSystem playerTrail;

    public float rotateSpeed = 5;

    private Vector3 _moveDirection = new Vector3(0, 0, 0);


    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _normalSpeed = speed;
    }

    private void Update()
    {
        if (!PlayerScriptor.Instance.AllowMoving)
            return;
        
        //Checks if the Player presses Shift to run
        CheckIfRunning();
        //Player Movement
        MovePlayer();
        //Checks if the Player is moving and if he does it emits the fairy dust particles
        if (IsMoving())
        {
            //playerTrail.Emit(1);
            Debug.Log("is moving");
            audioPlayer.Play();
        }
        else
        {
            //playerTrail.Stop();
            Debug.Log("is not moving");
            audioPlayer.Stop();
        }
    }

    //The Main movement method which checks Input and moves the Character depending on its position around the Tree 
    private void MovePlayer()
    {
        var step = rotateSpeed * Time.deltaTime;

        Vector3 step1, step2, step3;

        if (_controller.isGrounded)
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * speed;
            
            _moveDirection = cam.transform.TransformDirection(_moveDirection);
            _moveDirection.y = 0;

            //Move.Towards is for a smoother transition when the Model turns, so it doesn't turn instantly
            var newDir = Vector3.MoveTowards(transform.forward, _moveDirection, step);
            step3 = newDir;
            if (newDir != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(newDir);
            }
            

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
        //Check if the Player is moving on x and z axis
        var x = _moveDirection.x;
        var z = _moveDirection.z;

        return x >= 1 || z >= 1 || x <= -0.5 || z <= -0.5;
    }
}