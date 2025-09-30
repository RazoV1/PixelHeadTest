
using Unity.Cinemachine;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	[SerializeField] private Transform anchor;
	[SerializeField] private Transform target;
	[SerializeField] private Transform targetModel;
	[SerializeField] private Rigidbody rb;

	[SerializeField] private float rotationSpeed;

	[SerializeField] private float interpolationSpeed;

	private CameraState currentState = CameraState.following;

	public enum CameraState
	{
		following,
		pivoted
	}

	public void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		HandleCameraBehaviour();
	}

	private void HandleCameraBehaviour()
	{
		switch (currentState)
		{
			case CameraState.following:
				ChangeAnchorOrientation();
				TryChangePlayerModelOrientation();
				break;
			case CameraState.pivoted:
				HandlePivotedPosition();
				break;
		}
	}

	public void ChangeCameraState(CameraState state, Transform target, Transform anchor = null, Transform targetModel = null)
	{
		currentState = state;
		this.target = target;
		this.anchor = anchor;
		this.targetModel = targetModel;
	    
		GetComponentInParent<CinemachineBrain>().enabled = currentState == CameraState.following;

		if (currentState == CameraState.following)
		{
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.Euler(0, 0, 0);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	#region Following
	private void TryChangePlayerModelOrientation()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		Vector3 inputDir = anchor.forward * verticalInput + anchor.right * horizontalInput;

		if (inputDir != Vector3.zero)
		{
			if (!target.GetComponent<PlayerMovement>().CabMove()) return;
			targetModel.forward = Vector3.Slerp(targetModel.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
		}
	}

	private void ChangeAnchorOrientation()
	{
		Vector3 viewDir = target.position - new Vector3(
			transform.position.x,
			target.position.y,
			transform.position.z);
		anchor.forward = viewDir.normalized;
	}
	#endregion
	#region Pivoted
	public void HandlePivotedPosition()
	{
		transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * interpolationSpeed);
		transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * interpolationSpeed);
	}
	#endregion
}
