using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    MoveLeft moveLeft;

    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtParticle;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;

   readonly private float jumpForce = 700;
   readonly private float gravityModifier = 1.5f;

    public bool isOnGround = true;
    public bool gameOver = false;

    bool doubleJumpUsed = false;
    bool startAnimPlay = false;

    readonly float doubleJumpForce = 9;
 
    
    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        moveLeft = GameObject.FindAnyObjectByType<MoveLeft>();
    }
    void Start()
    {
        Physics.gravity *= gravityModifier;
    }
    void Update()
    {
        StartAnimation();
        Jump();
        Dash();
    }


    void StartAnimation()
    {
        if (!startAnimPlay)
        {
            gameOver = true;
            transform.Translate(Vector3.forward / 80);
            playerAnim.SetFloat("Speed_f", 0.3f);
            if (transform.position.x >= 0)
            {
                gameOver = false;
                playerAnim.SetFloat("Speed_f", 0.6f);
                startAnimPlay = true;
            }
        }

    }




    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerAnim.SetTrigger("Jump_trig");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            doubleJumpUsed = false;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJumpUsed)
        {
            doubleJumpUsed = true;
            playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", 3, 0f);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }



    void Dash()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveLeft.Speed = 50;
            playerAnim.Play("Run_Static", 0, 2f);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            gameOver = true;
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }

    }


}
