using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMovement : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;


    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    float horizontalInput;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        
    }
    void Update()
    {
        Movement();
        
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        #region Flip Character
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        #endregion
        if (wallJumpCooldown > 0.2f)
        {
           
            rb.velocity = new Vector2(horizontalInput * _speed, rb.velocity.y);
            if (OnWall() && !isGrounded())
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            }
            else
                rb.gravityScale = 1.5f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
            wallJumpCooldown += Time.deltaTime;


        // set animator
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", isGrounded());

    }

    void Jump()
    {
        if(isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpPower);
        }
        else if(OnWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
            else
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, _jumpPower);
            wallJumpCooldown = 0f;

        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return raycastHit.collider != null;
    }
    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool CanAttack()
    {
        return horizontalInput == 0 && isGrounded() && !OnWall();
    }    
}
