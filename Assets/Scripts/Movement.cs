using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float jumpForce = 10.0f;

    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private BoxCollider2D jumpTriggerBox2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime); // Constant forward movement

        OnTriggerEnter2D(boxCollider2D);
    }

    private bool IsGrounded()
    {
        float extraHeigthText = 0.2f;
        RaycastHit2D rayCastHit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down,
            boxCollider2D.bounds.extents.y + extraHeigthText, platformLayerMask);
        return rayCastHit.collider != null;
    }

    void Jump()
    {
        rigidbody2D.velocity = Vector2.up * jumpForce;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsGrounded() && collision.CompareTag("JumpTrigger"))
        {
            Jump();
        }
        else
        {
            Debug.Log("not triggered");
        }
    }
}

