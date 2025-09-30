using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] private float movementSpeed;
	[SerializeField] private float groundDrag;
	private float horizontalInput;
	private float verticalInput;
	private Vector3 moveDirection;
	private Rigidbody rb;
	[SerializeField] private Transform anchor;

	[Header("Ground Check")]
	private bool isGrounded;
	[SerializeField] private float playerHeight;
	[SerializeField] private LayerMask groundLayer;

	[Header("Animations")]
	[SerializeField] private PlayerAnimations playerAnimations;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
	}

	private void Update()
	{
		HandleInput();
		HandleAnimationStates();
		TryApplyGroundDrag();
		LimitPlayerSpeed();
		HandleAnimationStates();
	}

	private void FixedUpdate()
	{
		HandleMovement();
	}
	#region Movement

	private void HandleInput()
	{
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");
	}

	private void HandleMovement()
	{
		moveDirection = anchor.forward * verticalInput + anchor.right * horizontalInput;

		rb.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);
	}

	private void TryApplyGroundDrag()
	{
		isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
		if (isGrounded)
		{
			rb.linearDamping = groundDrag;
		}
		else
		{
			rb.linearDamping = 0;
		}
	}

	private void LimitPlayerSpeed()
	{
		rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, movementSpeed);
	}
	#endregion
	#region Animation
	private void HandleAnimationStates()
	{
		playerAnimations.ChangeStateVariable(
			"velocity", 
			rb.linearVelocity.magnitude/movementSpeed);
	}
	#endregion
}
