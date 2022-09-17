using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float horizontalMove = 0f;
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    private float movementSpeed = 1.0f;
    private float jumpForce = 10.0f;

    [SerializeField] private LayerMask platformLayerMask;

    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
       
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2D.velocity = Vector2.up * jumpForce;
       
        }
    }

    private bool IsGrounded()
    {
        float extraHeigthText = 0.01f;
        RaycastHit2D rayCastHit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down, boxCollider2D.bounds.extents.y + extraHeigthText);
        return rayCastHit.collider != null;
    }
}
