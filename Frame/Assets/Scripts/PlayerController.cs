using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour 
{
	[Header("Movement")]
	public float speed; 
	public float jumpForce;
	public Transform groundCheck;
	public LayerMask groundMask;

	bool canDoubleJump = true;
	bool isGrounded;

	[Header("SFX")]
	private AudioSource audioSource;
	public AudioClip jumpSfx;

	private Animator anim;
	private SpriteRenderer spriteRenderer;
	private Rigidbody2D body;

	bool finished = false;
	

	void Start()
	{

		body = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		audioSource = GetComponent<AudioSource> ();
	}
		
	void Update()
	{		
		if (Input.GetButtonDown("Jump") && (isGrounded || canDoubleJump) && !finished)
		{
			body.velocity = new Vector2 (body.velocity.x, jumpForce);
			anim.SetTrigger ("onJump");

			audioSource.PlayOneShot (jumpSfx, 0.25f);


			if (canDoubleJump && !isGrounded)
				canDoubleJump = false;
		}

		if (isGrounded)
		{
			canDoubleJump = true; 
		}
	}

	void FixedUpdate() 
	{
		if (Physics2D.OverlapCircle (groundCheck.position, 0.25f, groundMask) != null)
			isGrounded = true;
		else
			isGrounded = false;

		anim.SetBool ("isGrounded", isGrounded);

		//Déplacement
		if (!finished) 
		{
			float horizontal = Input.GetAxis ("Horizontal");
			body.velocity = new Vector2 (speed * horizontal, body.velocity.y);
		}
		else
			body.velocity = new Vector2 (0, body.velocity.y);

		anim.SetFloat ("horizontalSpeed", Mathf.Abs(body.velocity.x));
		anim.SetFloat ("verticalSpeed", body.velocity.y);

		if (body.velocity.x < 0)
			spriteRenderer.flipX = true;
		else if (body.velocity.x > 0)
			spriteRenderer.flipX = false;

	}
}
