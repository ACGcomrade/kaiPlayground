using System.Globalization;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    public float playerGravity = -9.81f;
    public float playerJumpHeight = 12f;

    public Transform checkLand;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGround;
    Vector3 velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics.CheckSphere(checkLand.position, groundDistance, groundMask);

        if (isGround && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGround)
        {
            velocity.y = Mathf.Sqrt(-2f * playerJumpHeight * playerGravity);
        }

        velocity.y += playerGravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}

