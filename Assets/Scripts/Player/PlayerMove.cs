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
    private bool isClimbing = false;

    private float horizontalMove;
    private float verticalMove;

    private Rigidbody rigidbody;
    private Vector3 movement;

    private Ray ray;
    private float distance = 0.5f;
    private RaycastHit[] rayHits;

    private Player player = null;
    private PlayerAnimController playerAnimController = null;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerAnimController = GetComponent<PlayerAnimController>();
        player = GetComponent<Player>();

        ray = new Ray(transform.position, transform.forward);
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
            isJumping = true;

        if (Input.GetMouseButtonDown(0))
        {
            player.playerState = PlayerState.Normal_Attack;
            playerAnimController.isAttacking = true;
        }

        UpdateRay();
        FindRayHits();
    }

    void FixedUpdate()
    {
        if(player.playerState == PlayerState.Climbing)
        {
            //isClimbing = true;
            Climbing(horizontalMove, verticalMove);
        }
        else if (!playerAnimController.isAttacking)
        {
            Walk(horizontalMove, verticalMove);
            Jump();
            Rotate();
        }
    }

    void Walk(float h, float v)
    {
        moveSpeed = 3.0f;
        rigidbody.useGravity = true;

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

    void Climbing(float h, float v)
    {
        moveSpeed = 1.5f;
        rigidbody.useGravity = false;

        movement.Set(h, v, 0);
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        rigidbody.MovePosition(transform.position + movement);
    }

    void UpdateRay()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;
    }

    void FindRayHits()
    {
        rayHits = Physics.RaycastAll(ray, distance);

        for (int i = 0; i < rayHits.Length; i++)
        {
            if (rayHits[i].collider.gameObject.CompareTag("Wall"))
            {
                player.playerState = PlayerState.Climbing;
                return;
            }
        }

        isClimbing = false;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, ray.direction * distance, Color.cyan);
    }
}
