using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditorInternal;
using UnityEngine;
public class Test1 : MonoBehaviour
{
    public Rigidbody2D rb;
    //public Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        GroundCheckRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
        WallCheckRadiusDown = WallCheckDown.GetComponent<CircleCollider2D>().radius;
        gravityDef = rb.gravityScale;
    }
    void Update()
    {
        Reflect();
        Jump();
        Walk();
        MoveOnWall();
        WallJump();
        LedgeGo();
    }
    private void FixedUpdate()
    {
        CheckingGround();
        CheckingWall();
        CheckingLedge();
    }
    public Vector2 moveVector;
    public int speed = 3;
    void Walk()
    {
        if (blockMoveXforJump || blockMoveXYforLedge)
        {
            moveVector.x = 0;
        }
        else
        {
            moveVector.x = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        }
        //anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
    }
    public bool faceRight = true;
    void Reflect()
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }

    public int jumpForce = 10;
    void Jump()
    {
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            //anim.StopPlayback();
            //anim.Play("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    public bool onGround;
    public LayerMask Ground;
    public Transform GroundCheck;
    private float GroundCheckRadius;
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, Ground);
        //anim.SetBool("onGround", onGround);
    }
    public bool onWall;
    public bool onWallUp;
    public bool onWallDown;
    public LayerMask Wall;
    public Transform WallCheckUp;
    public Transform WallCheckDown;
    public float WallCheckRayDistance = 1f;
    private float WallCheckRadiusDown;
    public bool onLedge;
    public float ledgeRayCorrectY = 0.5f;
    void CheckingWall()
    {
        onWallUp = Physics2D.Raycast
        (
        WallCheckUp.position,
        new Vector2(transform.localScale.x, 0),
        WallCheckRayDistance,
        Wall
        );
        onWallDown = Physics2D.OverlapCircle(WallCheckDown.position, WallCheckRadiusDown, Wall);
        onWall = (onWallUp && onWallDown);
        //anim.SetBool("onWall", onWall);
        //if (onWallUp && !onWallDown) { anim.SetBool("wallCheckUp", true); }
        //else { anim.SetBool("wallCheckUp", false); }
    }
    void CheckingLedge()
    {
        if (onWallUp)
        {
            onLedge = !Physics2D.Raycast
            (
            new Vector2(WallCheckUp.position.x, WallCheckUp.position.y + ledgeRayCorrectY),
            new Vector2(transform.localScale.x, 0),
            WallCheckRayDistance,
            Wall
            );
        }
        else { onLedge = false; }
        //anim.SetBool("onLedge", onLedge);

        if ((onLedge && Input.GetAxisRaw("Vertical") != -1) || blockMoveXYforLedge)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
            offsetCalculateAndCorrect();
        }
    }
    public float minCorrectDistance = 0.01f;
    public float offsetY;
    void offsetCalculateAndCorrect()
    {
        offsetY = Physics2D.Raycast
        (
        new Vector2(WallCheckUp.position.x + WallCheckRayDistance * transform.localScale.x,
        WallCheckUp.position.y + ledgeRayCorrectY),
        Vector2.down,
        ledgeRayCorrectY,
        Ground
        ).distance;
        if (offsetY > minCorrectDistance * 1.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - offsetY +
            minCorrectDistance, transform.position.z);
        }
    }
    private bool blockMoveXYforLedge;
    void LedgeGo()
    {
        if (onLedge && Input.GetKeyDown(KeyCode.UpArrow))
        {
            blockMoveXYforLedge = true;
            FinishLedge();// ВЫЗОВ
            //if (onWallUp && !onWallDown) { anim.Play("platformLedgeClimb"); }
            //else { anim.Play("wallLedgeClimb"); }
        }
    }
    public Transform finishLedgePosition;
    void FinishLedge()
    {
        transform.position = new Vector3(finishLedgePosition.position.x, finishLedgePosition.position.y,
        finishLedgePosition.position.z);
        //anim.Play("idle");
        blockMoveXYforLedge = false;
    }
    public float upDownSpeed = 4f;
    public float slideSpeed = 0;
    private float gravityDef;
    void MoveOnWall()
    {
        if (onWall && !onGround)
        {
            moveVector.y = Input.GetAxisRaw("Vertical");
            //anim.SetFloat("UpDown", moveVector.y);
            if (!blockMoveXforJump && moveVector.y == 0)
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(0, slideSpeed);

            }
            if (!blockMoveXYforLedge)
            {
                if (moveVector.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, moveVector.y * upDownSpeed / 2);
                }
                else if (moveVector.y < 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, moveVector.y * upDownSpeed);
                }
            }
        }
        else if (!onGround && !onWall) { rb.gravityScale = gravityDef; }
    }
    private bool blockMoveXforJump;
    public float jumpWallTime = 0.5f;
    private float timerJumpWall;
    public Vector2 jumpAngle = new Vector2(3.5f, 10);
    void WallJump()
    {
        if (onWall && !onGround && Input.GetKeyDown(KeyCode.Space))
        {
            blockMoveXforJump = true;
            moveVector.x = 0;
            //anim.StopPlayback();
            //anim.Play("wallJump");
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
            rb.gravityScale = gravityDef;
            rb.velocity = new Vector2(0, 0);
            rb.velocity = new Vector2(transform.localScale.x * jumpAngle.x, jumpAngle.y);
        }
        if (blockMoveXforJump && (timerJumpWall += Time.deltaTime) >= jumpWallTime)
        {
            if (onWall || onGround || Input.GetAxisRaw("Horizontal") != 0)
            {
                blockMoveXforJump = false;
                timerJumpWall = 0;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine
        (
        WallCheckUp.position,
        new Vector2(WallCheckUp.position.x + WallCheckRayDistance * transform.localScale.x,
        WallCheckUp.position.y)
        );
        Gizmos.color = Color.red;
        Gizmos.DrawLine
        (

        new Vector2(WallCheckUp.position.x, WallCheckUp.position.y + ledgeRayCorrectY),
        new Vector2(WallCheckUp.position.x + WallCheckRayDistance * transform.localScale.x,
        WallCheckUp.position.y + ledgeRayCorrectY)
        );
        Gizmos.color = Color.green;
        Gizmos.DrawLine
        (
        new Vector2(WallCheckUp.position.x + WallCheckRayDistance * transform.localScale.x,
        WallCheckUp.position.y + ledgeRayCorrectY),
        new Vector2(WallCheckUp.position.x + WallCheckRayDistance * transform.localScale.x,
        WallCheckUp.position.y)
        );
    }
}
