using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Animator animator;
    private Player player = null;

    private float h;
    private float v;

    private bool isJumping = false;
    public bool isAttacking = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
            isJumping = true;

        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            PlayAttack();
        }

        if(Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Dash");
        }

        if (h == 0 && v == 0 && player.playerState == PlayerState.Climbing)
            animator.speed = 0;
        else
            animator.speed = 1;
 
        if(player.playerState == PlayerState.InMenu)
        {
            animator.SetBool("Walk", false);
            this.enabled = false;
        }

        if(player.playerState == PlayerState.Climbing)
            animator.SetBool("Climbing", true);
        else
            animator.SetBool("Climbing", false);

        if (player.playerState == PlayerState.Running)
        {
            animator.SetBool("Run", true);
            animator.SetBool("Walk", false);
        }
        else
            animator.SetBool("Run", false);

        /*skill*/
        if (Input.GetKey(KeyCode.E) && player.playerState == PlayerState.Elemental_Skill)
            animator.SetBool("PalmVortex", true);
        else
            animator.SetBool("PalmVortex", false);

        if(Input.GetKey(KeyCode.Q) && player.playerState == PlayerState.Elemental_Burst)
            animator.SetBool("GustSurge", true);

        if (h == 0 && v == 0)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);

            if(player.playerState != PlayerState.Normal_Attack)
                player.playerState = PlayerState.None;
        }
    }

    void FixedUpdate()
    {
        if(!isAttacking)
        {
            PlayWalk(h, v);
            PlayJump();
        }
    }

    void PlayWalk(float h, float v)
    {
        animator.SetBool("Walk", true);
    }

    void PlayJump()
    {
        if (isJumping == false)
            return;

        animator.SetTrigger("Jump");
        isJumping = false;
    }

    void PlayAttack()
    {
        animator.SetTrigger("Slash");
        //rigidbody.MovePosition(transform.position + transform.forward * 0.8f);
    }

    public void ExitSlash1()
    {
        isAttacking = false;
        player.playerState = PlayerState.None;
    }

    public void ExitGustSurge()
    {
        animator.SetBool("GustSurge", false);
        player.playerState = PlayerState.None;
    }
}
