using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] private float movementSpeed;
	[SerializeField] private float groundDrag;

	[Header("Ground Check")]
	private bool isGrounded;
	[SerializeField] private float playerHeight;
	[SerializeField] private LayerMask groundLayer;

	[SerializeField] private Transform anchor;

	private float horizontalInput;
	private float verticalInput;

	private Vector3 moveDirection;

	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
	}

	private void Update()
	{
		HandleInput();
		HandleMovement();
		TryApplyGroundDrag();
		LimitPlayerSpeed();
	}

	private void HandleInput()
	{
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");
	}

	private void HandleMovement()
	{
		moveDirection = anchor.forward * verticalInput + anchor.right * horizontalInput;

		rb.AddForce(moveDirection.normalized * movementSpeed * 10f * Time.deltaTime, ForceMode.Force);
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
}
