using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movimiento con:
/// 
/// Transform: Te da libertad en lo que quieras hacer, te limita las detecciones
/// Character Controller: Te da facilidad de movimiento y deteccion, pero te limita las fisicas
/// RigidBody: Te da facilidad para movimiento y deteccion, pero requiere mayor trabajo para lograr precision
/// 
/// </summary>
public class MovementController : MonoBehaviour
{

    [SerializeField] private float crouchSpeed = 4;
    [SerializeField] private float walkspeed = 6;
    [SerializeField] private float runSpeed = 8;

    [SerializeField] private float jumpForce = 5.3f;

    private Rigidbody rb;

    private Respawn respawn;


    private void Start()
    {
        respawn = FindObjectOfType<Respawn>();
        rb = rb == null ? GetComponent<Rigidbody>() : rb;
    }

    private void Update()
    {
        Jump();
    }
    private void FixedUpdate()
    {
        Move();
        
    }

    private void Move()
    {
        Vector3 horizontalMove = transform.rotation * new Vector3(HorizontalAxis(), 0, VerticalAxis());
        Vector3 targetVelocity = horizontalMove * Speed();
        rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
    }

    public float Speed()
    {
        if (RunInputPressed())
        {
            return runSpeed;
        }
        else if (CrouchInputPressed())
        {
            return crouchSpeed;
        }
        return walkspeed; 
    }

    private void Jump()
    {
        if (JumpInputPressed())
        {
            Debug.Log("Saltando");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
        }
    }

    private float HorizontalAxis()
    {
        return Input.GetAxis("Horizontal");
    }

    private float VerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }

    public bool JumpInputPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool RunInputPressed()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool CrouchInputPressed()
    {
        return Input.GetKey(KeyCode.LeftControl);
    }
}