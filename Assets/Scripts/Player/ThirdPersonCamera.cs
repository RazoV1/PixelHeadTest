using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	[SerializeField] private Transform anchor;
	[SerializeField] private Transform player;
	[SerializeField] private Transform playerObj;
	[SerializeField] private Rigidbody rb;

	[SerializeField] private float rotationSpeed;

	public void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		ChangeAnchorOrientation();
		TryChangePlayerModelOrientation();
	}

	private void TryChangePlayerModelOrientation()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		Vector3 inputDir = anchor.forward * verticalInput + anchor.right * horizontalInput;

		if (inputDir != Vector3.zero)
		{
			playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
		}
	}

	private void ChangeAnchorOrientation()
	{
		Vector3 viewDir = player.position - new Vector3(
			transform.position.x,
			player.position.y,
			transform.position.z);
		anchor.forward = viewDir.normalized;
	}
}
