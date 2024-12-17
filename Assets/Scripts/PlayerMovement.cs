using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    public Vector3 spawnPoint;
    public SpriteRenderer sprite;
    public bool inputEnabled = true;
    //private SpriteRenderer sprite;

    private Vector3 startPosition;

    [SerializeField]
    [Range(0, 20)]
    private float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        //sprite = GetComponent<SpriteRenderer>();

    }

    float previousX = 0;
    // Update is called once per frame
    void Update()
    {
        if (inputEnabled && animator.GetBool("CanMove") != (false == true))
        {
            var horizontalAxis = Input.GetAxisRaw("Horizontal");
            var verticalAxis = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector3(horizontalAxis * speed, 0, verticalAxis * speed);
            if (horizontalAxis < 0)
            {
                sprite.flipX = true;
            }
            else if (horizontalAxis > 0)
            {
                sprite.flipX = false;
            }
            if (horizontalAxis != 0 || verticalAxis != 0) { animator.SetTrigger("IsMoving"); }
            else animator.ResetTrigger("IsMoving");
        }

        if (Mathf.Abs(transform.position.x - previousX) > 0.01f)
        {
            if (transform.position.x > previousX)
            {
                sprite.flipX = false;
            }
            else if (transform.position.x < previousX)
            {
                sprite.flipX = true;
            }
            previousX = transform.position.x;
        }
    }

    public void Respawn()
    {
        transform.position = spawnPoint;
        animator.ResetTrigger("Falls");
    }

    public void ResetPlayerPosition()
    {
        transform.position = startPosition;
        animator.ResetTrigger("Falls");
    }
}
