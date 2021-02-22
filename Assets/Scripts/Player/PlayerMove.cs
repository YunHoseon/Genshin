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
    private bool isDashing = false;

    private float horizontalMove;
    private float verticalMove;

    private Rigidbody rigidbody;
    private Vector3 movement;

    private Ray ray;
    private float distance = 0.8f;
    private RaycastHit[] rayHits;

    private Player player = null;
    private PlayerAnimController playerAnimController = null;

    private float dashTimer = 0.3f;
    private float dashStamina = 15.0f;
    private float runStamina = 10.0f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerAnimController = GetComponent<PlayerAnimController>();
        player = GetComponent<Player>();

        ray = new Ray(transform.position, transform.forward);
        player.PlayerStamina = player.PlayerMaxStamina;
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

         if (Input.GetMouseButtonDown(1) && player.PlayerStamina > dashStamina)
         {
            Dash();

            if (player.PlayerStamina > 0.0f)
                player.playerState = PlayerState.Running;
            else
                player.playerState = PlayerState.None;
         }
        
        UpdateRay();
        FindRayHits();

        if (player.PlayerStamina <= 0.0f)
        {
            player.PlayerStamina = 0;
            player.playerState = PlayerState.None;
        }

        CheckIsDash();
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
        isDashing = true;
        player.PlayerStamina -= dashStamina;
    }

    void CheckIsDash()
    {
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0.0f)
            {
                isDashing = false;
                dashTimer = 0.3f;
            }
        }
    }

    void Run(float h, float v)
    {
        if (!isDashing)
            moveSpeed = 4.0f;
        else
            moveSpeed = 10.0f;

        player.PlayerStamina -= runStamina * Time.deltaTime;

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

        if(player.playerState == PlayerState.Climbing)
            player.playerState = PlayerState.None;
    }

    void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("Wall"))
        {
            Debug.Log("등반중");
            isClimbing = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Wall"))
            isClimbing = false;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, ray.direction * distance, Color.cyan);
    }
}
