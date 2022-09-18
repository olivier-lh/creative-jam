using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    public Animator animator;
    private GameManager gameManager;
    
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float jumpForce = 10.0f;

    [SerializeField] private LayerMask platformLayerMask;

    Vector2 characterDirection = new Vector2(1, 0);
    private bool canLand = false;
    
    [SerializeField] private BoxCollider2D jumpTriggerBox2D;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        rigidbody2D.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.getAttemptIsStarted())
        {
            transform.Translate(characterDirection * movementSpeed * Time.deltaTime);
            animator.SetFloat("Speed", Mathf.Abs(movementSpeed));
        }
    }

    private bool IsGrounded()
    {
        float extraHeigthText = 0.2f;
        RaycastHit2D rayCastHit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down,
            boxCollider2D.bounds.extents.y + extraHeigthText);
        return rayCastHit.collider != null;
    }

    //OnTriggerEnter function to deal with all the triggers
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (canLand && other.CompareTag("Ground"))
        {
            canLand = false;
            animator.SetBool("IsJumping", false);
        }
        
        if (other.CompareTag("DeathZone"))
        {
            Destroy(this.gameObject);
            gameManager.RespawnPlayer();
        }
        
        if (other.CompareTag("JumpTrigger"))
        {
            Jump();
            animator.SetBool("IsJumping", true);
            canLand = true;
        }

        if(other.CompareTag("FlipTrigger"))
        {
            Debug.Log(other.name);
            characterDirection.x *= -1;
            Vector3 inversedScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            transform.localScale = inversedScale;
        }
    }
    
    void Jump()
    {
        rigidbody2D.velocity = Vector2.up * jumpForce;
    }
}

