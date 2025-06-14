using UnityEngine;

namespace Assets.Scripts
{

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce; // Adjustable jump force
        private UnityEngine.Rigidbody2D body;
        private Animator anim;
        private bool grounded;

        [Header("Audio")]
        [SerializeField] private AudioClip jumpSFX;
        [SerializeField] private AudioClip landSFX;
        private AudioSource audioSource;

        private void Awake()
        {
            //Grab references for Ridgedbody2D and Animator from object
            body = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>(); // Get AudioSource
        }

        private void FixedUpdate()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            // even though tutorial says to have at as velocity, vs changed it to linearVelocity automatically.
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y); // use arrows or AWSD

            //Flip player when moving left-right
            if (horizontalInput > 0.01f)
                transform.localScale = Vector3.one;
            else if (horizontalInput < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);

            if (Input.GetKey(KeyCode.Space) && grounded)
                Jump();

            // Set animator parameters
            anim.SetBool("Grounded", grounded);

            // Transition to fall animation if not grounded and falling
            if (!grounded && body.linearVelocity.y < -0.1f)  // Ensure player is falling
            {
                anim.SetFloat("yVelocity", body.linearVelocity.y); // Set falling animation
            }
            else if (grounded)
            {
                anim.SetFloat("yVelocity", 0); // Reset yVelocity to 0 for idle animation when grounded
            }

            // Running animation condition
            if (grounded && Mathf.Abs(horizontalInput) > 0.01f)
            {
                anim.SetBool("Run", true);
            }
            else if (grounded)
            {
                anim.SetBool("Run", false);
            }
        }

        private void Jump()
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce); // Apply jump force to the y-axis
            anim.SetTrigger("Jump");
            grounded = false;


            // Play jump sound
            if (jumpSFX != null)
                audioSource.PlayOneShot(jumpSFX);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
                grounded = true;

            
                // Play land sound
                //if (landSFX != null)
                //    audioSource.PlayOneShot(landSFX);
        }
    }
}