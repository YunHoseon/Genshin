using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    private float jumpPower = 10.0f;
    [SerializeField]
    private float rotateSpeed = 5.0f;

    private bool isGround = true;
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

    private float intervalTime = 2.0f;

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

         if (Input.GetMouseButtonDown(1))
        {
            intervalTime = 2.0f;
            if (player.playerStamina_ > 0)
            {
                Dash();
                if (player.playerStamina_ > 0)
                {
                    player.playerState = PlayerState.Running;
                }
            }
        }

        if(player.playerState != PlayerState.Running)
        {
            intervalTime -= Time.deltaTime;

            if (intervalTime <= 0.0f && player.playerStamina_ < player.PlayerMaxStamina)
                player.playerStamina_ += 0.2f;
        }

        Debug.Log(player.playerStamina_);
        UpdateRay();
        FindRayHits();
    }

    void FixedUpdate()
    {
        if(player.playerState == PlayerState.Climbing)
        {
            Climbing(horizontalMove, verticalMove);
        }
        else if(player.playerState == PlayerState.Running)
        {
            Run(horizontalMove, verticalMove);
            Jump();
            Rotate();
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
        moveSpeed = 2.0f;
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
        moveSpeed = 1.0f;
        rigidbody.useGravity = false;

        movement.Set(h, v, 0);
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        rigidbody.MovePosition(transform.position + movement);
    }

    void Dash()
    {
        rigidbody.MovePosition(transform.position + transform.forward * 0.8f);
        player.playerStamina_ -= 1.0f;
    }

    void Run(float h, float v)
    {
        moveSpeed = 4.0f;

        movement.Set(h, 0, v);
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
            if (rayHits[i].collider.gameObject.CompareTag("Wall") && isClimbing)
            {
                player.playerState = PlayerState.Climbing;
                return;
            }
        }
        isClimbing = false;
    }

    void OnTriggerEnter(Collider col)
    {
        //if(col.CompareTag("Ground"))
            //isGround = true;

        if(col.CompareTag("Wall"))
            isClimbing = true;
    }

    void OnTriggerExit(Collider col)
    {
        //if (col.CompareTag("Ground"))
        //isGround = false;

        if (col.CompareTag("Wall"))
            isClimbing = false;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, ray.direction * distance, Color.cyan);
    }
}
