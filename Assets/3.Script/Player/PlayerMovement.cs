using System.Collections;
using UnityEngine;
// using 최영일;
// using ♡♡♡♡♡♡나태해졌으니 때려주세요 교수님 ♡♡♡♡♡♡;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float jumpForce;
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private float maxSlopeAngle;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D fullFriction;
    [Space(10f)]

    private float xInput;
    private float slopeDownAngle;
    private float slopeSideAngle;
    private float lastSlopeAngle;

    private bool isGrounded;
    private bool isOnSlope;
    private bool isJumping;
    private bool canWalkOnSlope;

    private Vector2 newVelocity;
    private Vector2 newForce;
    private Vector2 capsuleColliderSize;

    private Vector2 slopeNormalPerp;
    
    [Header("Action")]
    [SerializeField] InGameCursor inGameCursor;
    [SerializeField] private GameObject dashEffect;
    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private PlayerController playerController;
    private Animator Player_anim;
    private PlayerSound Player_sound;

    [SerializeField] private float DashDist;
    private Vector3 dashDirection;
    private bool isDashing = false;

    private int playerLayer;
    private int platformLayer;

    private int jumpCount;
    private bool isJumpOff;

    private bool isStepPlaying = false;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        TryGetComponent(out playerController);
        capsuleColliderSize = cc.size;
        playerLayer = LayerMask.NameToLayer("Player");
        platformLayer = LayerMask.NameToLayer("Platform");
        TryGetComponent(out Player_anim);
        TryGetComponent(out Player_sound);
        dashEffect.SetActive(false);
    }

    private void Update()
    {
        CheckInput();

        if (xInput != 0) { Player_anim.SetBool("Run", true); if (!isStepPlaying) StartCoroutine("StepSound_co"); }
        else { Player_anim.SetBool("Run", false); if (isStepPlaying) { StopCoroutine("StepSound_co"); isStepPlaying = false; } }

        // 땅에 닿으면 점프 꺼지는데 ground 태그로 판정
        // isgrounded로 언제나 점프 애니메이션 조절중 , layertag로 판정
        // platform은 하향점프가 가능해짐
        // 언제나 ground tag로 판정하면 되지않나

        if (!isJumpOff)
        {
            if (!isGrounded)
            {
                //Player_anim.SetBool("Jump", true);
                Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
            }
            else
            {
                //Player_anim.SetBool("Jump", false);
                if (!isDashing)
                    Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
            }
        }
    }

    private void FixedUpdate()
    {
        CheckGround();
        SlopeCheck();
        if (!isDashing)
            ApplyMovement();
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetMouseButtonDown(1) && playerController.dashCount > 0)
        {
            dashDirection = (inGameCursor.position - transform.position).normalized;
            StartCoroutine("Dash_co");
        }

        if (Input.GetButtonDown("Jump") && !Input.GetKey(KeyCode.S))
        {
            Jump();
        }
        else if (Input.GetButton("Jump") && Input.GetKey(KeyCode.S))
        {
            StartCoroutine("JumpOff_co");
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (rb.velocity.y <= 0.0f)
        {
            isJumping = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, capsuleColliderSize.y / 2));

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, whatIsGround);

        if (slopeHitFront)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);

        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != lastSlopeAngle)
            {
                isOnSlope = true;
            }

            lastSlopeAngle = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }

        if (isOnSlope && canWalkOnSlope && xInput == 0.0f)
        {
            rb.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
    }

    private void Jump()
    {
        Player_sound.PlayJump();
        Player_anim.SetBool("Jump", true);
        if (jumpCount < 2)
        {
            jumpCount++;
            isJumping = true;
            newVelocity.Set(0.0f, 0.0f);
            rb.velocity = newVelocity;
            newForce.Set(0.0f, jumpForce);
            rb.AddForce(newForce, ForceMode2D.Impulse);
        }
    }

    private IEnumerator JumpOff_co()
    {
        isJumpOff = true;
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
        //cc.isTrigger = true;
        rb.gravityScale = 15f;
        yield return new WaitForSeconds(0.3f);
        rb.gravityScale = 10f;
        //cc.isTrigger = false;
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
        isJumpOff = false;
    }

    private void ApplyMovement()
    {
        if (isGrounded && !isOnSlope && !isJumping)
        {
            newVelocity.Set(movementSpeed * xInput, 0.0f);
            rb.velocity = newVelocity;
        }
        else if (isGrounded && isOnSlope && canWalkOnSlope && !isJumping)
        {
            newVelocity.Set(movementSpeed * slopeNormalPerp.x * -xInput, movementSpeed * slopeNormalPerp.y * -xInput);
            rb.velocity = newVelocity;
        }
        else if (!isGrounded)
        {
            newVelocity.Set(movementSpeed * xInput, rb.velocity.y);
            rb.velocity = newVelocity;
        }
    }

    private IEnumerator Dash_co()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
        Player_sound.PlayDash();
        dashEffect.SetActive(true);
        isDashing = true;
        playerController.dashCount--;
        rb.gravityScale = 0f;
        rb.velocity = dashDirection * DashDist;
        yield return new WaitForSeconds(0.2f);
        dashEffect.SetActive(false);
        rb.gravityScale = 10f;
        rb.velocity = Vector3.zero;
        isDashing = false;
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Player_anim.SetBool("Jump", false);
            jumpCount = 0;
        }
    }

    private IEnumerator StepSound_co()
    {
        isStepPlaying = true;
        Player_sound.PlayStep();
        yield return new WaitForSeconds(0.3f);
        isStepPlaying = false;
    }
}