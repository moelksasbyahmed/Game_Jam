using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.XR;

public class movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private collision coll;
    public bool wallJumping;
    public float dashTime = 1.4f;
    public float dashSpeed = 30;
    public float dashDrag = 10;
    public float wallJumpTime = 1.4f;
    public float jumpLerp;
    public float jumpSpeed = 10;
    public float slideSpeed = 10;
    public float speed = 10;

    public float fallMuliplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float risiingMultiplier = 2f;

    bool canDash = true;
    bool canMove = true;
    float x, y;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<collision>();
    }
    IEnumerator DashWait(bool stopDash)
    {
        if(stopDash)
        canDash = false;
        canMove = false;
        rb.drag = dashDrag;
        yield return new WaitForSeconds(dashTime);
        wallJumping = false;
        canMove = true;
        rb.drag = 0;

    }
    IEnumerator wallJumpMovementDisable() {
        canMove = false;
        yield return new WaitForSeconds(wallJumpTime);
        canMove = true;
        //wallJumping = false;
    }
    void WallJump()
    {
        if (canMove && !coll.onGround && coll.onWall && Input.GetButtonDown("Jump"))
        {
            Vector2 dir;
            if (coll.onLeftWall)
            {
                dir = Vector2.right;
            }
            else
            {
                dir = Vector2.left;
            }
            wallJumping = true;
            jumpTo((dir + Vector2.up).normalized);
            canMove = false;
            StartCoroutine(wallJumpMovementDisable());
        }
    }
    void WallSlide()
    {
       
        if (coll.onWall)
        {
            bool pushingWall = false;
            // While im holding on the wall keep me sliding
            // if i stop pushing just let me fall
            if ((x < 0 && coll.onLeftWall) || (x > 0 && coll.onRightWall))
            {
                pushingWall = true;
            }
            if (pushingWall && !wallJumping)
            {
                rb.velocity = new Vector2(0, -slideSpeed);
            }

        }
    }
    public void Dash(bool stopDash)
    {
        if (!canDash) { return; }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.velocity = Vector2.zero;
            rb.velocity += new Vector2(x, y).normalized * dashSpeed;
            StartCoroutine(DashWait(stopDash));


        }
    }
    void Update()
    {

        WallSlide();
        Jump();
        Dash(true);
        WallJump();
        Walk();

    }
    void jumpTo(Vector2 dir)
    {
        // stop vertical movement
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpSpeed;

    }
    void Jump()
    {
        bool pushingWall = false;
        // While im holding on the wall keep me sliding
        // if i stop pushing just let me fall
        if ((x < 0 && coll.onLeftWall) || (x > 0 && coll.onRightWall))
        {
            pushingWall = true;
        }
        if (coll.onGround)
        {
            wallJumping = false;
            canDash = true;
        }
        if (coll.onGround && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y *(fallMuliplier-1) * Time.deltaTime;
        }
       /* else if (rb.velocity.y > 0 && Input.GetButton("Jump"))
        {
            // jumpSpeed * risingMultiplier = lowJumMultiplier-1
            rb.velocity += Vector2.up * Physics2D.gravity.y * ((lowJumpMultiplier/(rb.velocity.y))) * Time.deltaTime;
            // 9  8  7  6 5 4 3 2 1 0
            // 19 18 17 6 5 4 3 2 1 0
        }*/

        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        }

    }
    private void Walk()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);
        if (wallJumping)
        {
            rb.velocity = Vector2.Lerp(rb.velocity,new Vector2(dir.x * speed , rb.velocity.y), jumpLerp * Time.deltaTime);

        }
        else if(canMove && !wallJumping)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
            //rb.velocity = Vector2.right * dir.x * speed;
            // (1,0)
        }

    }
}
