using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    public Vector3 spawnPoint;

    [SerializeField]
    [Range(0, 20)]
    private float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetBool("CanMove") != (false == true)){
            rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, Input.GetAxisRaw("Vertical") * speed);
        }
        
    }

    public void Respawn(){
        transform.position = spawnPoint;
    }
}
