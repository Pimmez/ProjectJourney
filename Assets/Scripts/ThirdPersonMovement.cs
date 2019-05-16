using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float jumpHeight = 2f;
	[SerializeField] private int maxJumpAmount = 1;
	[SerializeField] private float gravity = -9.81f;
	[SerializeField] private float groundDistance = 0.2f;
	[SerializeField] private LayerMask groundLayer;

	[Space]

	[SerializeField] private Vector3 drag;


	private int currentJumpAmount = 1;
	private CharacterController charController;
	private Vector3 velocity;
	private bool isGrounded = true;
	private Transform groundChecker;

	private void Start()
	{
		charController = GetComponent<CharacterController>();
		groundChecker = transform.GetChild(0);
	}

	private void Update()
	{
		HandleMovement();
	}

	private void HandleMovement()
	{
		isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundLayer, QueryTriggerInteraction.Ignore);
		CheckIfGrounded();

		Vector3 _move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
		charController.Move(_move * Time.deltaTime * moveSpeed);

		Vector3 _movem = new Vector3(0, 0, Input.GetAxis("Vertical"));
		charController.Move(_movem * Time.deltaTime * moveSpeed);


		if (_move != Vector3.zero)
			transform.forward = _move;

		//Jump With DoubleJump

		if (Input.GetKeyDown(KeyCode.Space) && currentJumpAmount > 0)
			Jump();

		velocity.y += gravity * Time.deltaTime;
		velocity.x /= 1 + drag.x * Time.deltaTime;
		velocity.y /= 1 + drag.y * Time.deltaTime;

		charController.Move(velocity * Time.deltaTime);
	}

	/// <summary>

	/// Launches the player upwards according to the amount of jumps available.

	/// </summary>

	private void Jump()
	{
		velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		currentJumpAmount--;
	}

	/// <summary>

	/// Checks if the player is currently touching the ground (not in the air).

	/// </summary>

	private void CheckIfGrounded()
	{ 
		if (isGrounded && velocity.y < 0)
		{
			velocity.y = 0f;
			currentJumpAmount = maxJumpAmount;
		}
	}
}