using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Animator animator;
    private Player player = null;

    private float h;
    private float v;

    private bool isJumping = false;
    public bool isAttacking = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
            isJumping = true;

        if(Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            PlayAttack();
        }

        if (h == 0 && v == 0)
            animator.SetBool("Walk", false);

        if(player.playerState == PlayerState.InMenu)
        {
            animator.SetBool("Walk", false);
            this.enabled = false;
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
    }

    public void ExitSlash1()
    {
        isAttacking = false;
        player.playerState = PlayerState.None;
    }
}
