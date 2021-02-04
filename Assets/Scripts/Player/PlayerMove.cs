using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

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
    private Vector3 movement;

    private Player player = null;
    private PlayerAnimController playerAnimController = null;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerAnimController = GetComponent<PlayerAnimController>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
            isJumping = true;

        if (Input.GetMouseButtonDown(0))
        {
            player.playerState = PlayerState.Attaking;
            playerAnimController.isAttacking = true;
        }
    }

    void FixedUpdate()
    {
        if(!playerAnimController.isAttacking)
        {
            Walk(horizontalMove, verticalMove);
            Jump();
            Rotate();
        }
    }

    void Walk(float h, float v)
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        rigidbody.MovePosition(transform.position + movement);
    }

    void Jump() //한번만 되게 해야함
    {
        if(isJumping == false)
            return;

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
