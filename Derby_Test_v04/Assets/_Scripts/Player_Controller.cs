using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    public float speed;
    public float jumpForce = 10f;
    public float gravity = 14.0f;
    //public float maxSpeed;
    //public float accelFactor;

    private float verticalVelocity;

    public Vector3 movement { set; get; }

    private Transform camTransform;
   // private float currentSpeed;
    private Rigidbody myRB;
    private SphereCollider playerCollider;
   // private bool isGrounded = true;

	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody>();
        playerCollider = GetComponent<SphereCollider>();
    }
	
	// Update is called once per frame
	void Update () {

        //PLAYER JUMP
        if (IsPlayerGrounded())
        {
            //Apply Gravity Before Takeoff
            verticalVelocity = -gravity * Time.deltaTime;

            //Takeoff
           if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            //When off the ground, gravity starts working
            verticalVelocity -= gravity * Time.deltaTime;
        }

    //Get Player's Input In Reference To The World Axis
        movement = PoolInput();

    //Turn Movement In Reference To Camera Direction
        movement = RotateWithView();

        movement.Set(movement.x, verticalVelocity, movement.z);

    //Apply Movement To Player's Rigidbody
        myRB.velocity = movement * speed;

        Debug.Log(movement);

	}

    private Vector3 PoolInput()
    {
        Vector3 dir = Vector3.zero;

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        if (dir.magnitude > 1)
            dir.Normalize();

        return dir;
    }

    private Vector3 RotateWithView()
    {
        if (camTransform != null)
        {
            Vector3 dir = camTransform.TransformDirection(movement);
            dir.Set(dir.x, 0f, dir.z);
            return dir.normalized * movement.magnitude;
        }
        else
        {
            camTransform = Camera.main.transform;
            return movement;
        }
    }

    private bool IsPlayerGrounded()
    {
        Vector3 leftRayStart;
        Vector3 rightRayStart;

        leftRayStart = playerCollider.bounds.center;
        rightRayStart = playerCollider.bounds.center;

        leftRayStart.x -= playerCollider.bounds.extents.x;
        rightRayStart.x += playerCollider.bounds.extents.x;

        Debug.DrawRay(leftRayStart, Vector3.down, Color.red);
        Debug.DrawRay(rightRayStart, Vector3.down, Color.green);

        if (Physics.Raycast(leftRayStart, Vector3.down, (playerCollider.radius) + 2f))
            return true;

        if (Physics.Raycast(rightRayStart, Vector3.down, (playerCollider.radius) + 2f))
            return true;

        else
            return false;
    }
}
