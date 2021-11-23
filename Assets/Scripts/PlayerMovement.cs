using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    bool isGround = true;
    bool doubleJumpAllowed = false;
    bool attackAllowed = true;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;


    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    float horizontalInput;

    public GameObject checkAttack;
    public bool isAttack;
    public int damagePlayer;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        Movement();
        Jump();
        if (Input.GetMouseButtonDown(0))
            Attack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Ground");
            isGround = true;
            doubleJumpAllowed = true;
        }
    }
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * _speed, rb.velocity.y);
        #region Flip Character
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        #endregion
       
        // set animator
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", isGround);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpPower);
            isGround = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJumpAllowed)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpPower);
            doubleJumpAllowed = false;
        }
    }
    void Attack()
    {
        if(!isAttack)
        {
            anim.SetTrigger("Attack");
            StartCoroutine(colAttack());
        }
        
    } 
    IEnumerator colAttack()
    {
        checkAttack.SetActive(true);
        isAttack = true;
        Debug.Log("Take Attack");
        yield return new WaitForSeconds(0.6f);
        checkAttack.SetActive(false);
        isAttack = false;
    }
   
}
