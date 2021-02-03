using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3.0f;
    [SerializeField]
    private float jumpPower = 10.0f;
    [SerializeField]
    private float rotateSpeed = 5.0f;

    private bool isJumping = false;
    private float horizontalMove;
    private float verticalMove;

    private Rigidbody rigidbody;
    private Animator animator;
    private Vector3 movement;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        if (horizontalMove == 0 && verticalMove == 0)
            animator.SetBool("Walk", false);
    }

    void FixedUpdate()
    {
        Walk(horizontalMove, verticalMove);
        Jump();
        Rotate();
    }

    void Walk(float h, float v)
    {
        animator.SetBool("Walk", true);
        movement.Set(h, 0, v);
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        rigidbody.MovePosition(transform.position + movement);
    }

    void Jump() //한번만 되게 해야함
    {
        if(isJumping == false)
            return;

        animator.SetTrigger("Jump");
        rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isJumping = false;
    }

    void Rotate()
    {
        if (horizontalMove == 0 && verticalMove == 0)
            return;

        Quaternion newRotation = Quaternion.LookRotation(movement);
        rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }
}
