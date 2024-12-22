using System;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    public Vector3 spawnPoint;
    public SpriteRenderer sprite;

    public AudioSource audioSource;
    public HandController handController;
    public bool inputEnabled = true;

    public bool tripping = false;
    //private SpriteRenderer sprite;
    public bool canTrip = false;
    public float tripCD = 0f;

    private Vector3 startPosition;

    [SerializeField]
    [Range(0, 20)]
    private float speed = 10;

    private float TrueSpeed
    {
        get
        {
            float result = speed;
            if (Input.GetKey(KeyCode.LeftShift) && UpgradeHandler.Instance.DexterityLevel >= 1)
            {
                result *= 1.75f;
                tripCD -= Time.deltaTime;
                if(tripCD <= 0)
                {
                    tripCD = .5f;
                    if (UnityEngine.Random.Range(0f, 100f) > (85f + (UpgradeHandler.Instance.DexterityLevel*3)))
                    {
                        Trip();
                    }
                }
                    
            }
            return result;
        }
    }

    public void Trip()
    {
        tripping = true;
        Debug.Log("Tripping");
        animator.SetTrigger("Trips");
        rb.velocity = new Vector3(0, 0, 0);
        audioSource.Play();
        if(handController.HasMug()){
            int mugs = handController.heldMugs;
            for (int i = 0; i < mugs; i++)
            {
                DroppedBeerInteractable.Spawn(transform.position);
                FindAnyObjectByType<HandController>().ReleaseMug();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        handController = FindAnyObjectByType<HandController>();
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
            float endSpeed = TrueSpeed;
            rb.velocity = new Vector3(horizontalAxis * endSpeed, 0, verticalAxis * endSpeed);
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
        }else if(!animator.GetBool("CanMove")){
            rb.velocity = new Vector3(0, 0, 0);
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
        animator.Rebind();
        animator.Update(0f);

        transform.position = startPosition;
        animator.ResetTrigger("Falls");
    }
}
