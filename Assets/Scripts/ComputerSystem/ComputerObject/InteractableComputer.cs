using UnityEngine;

public class InteractableComputer : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject computerGUI;
	[SerializeField] private Transform cameraPivotPoint;
	private bool isBeingUsed = false;

	private bool isInRange;
	private PlayerMovement playerInRange;

	private void OnTriggerEnter(Collider other)
	{
		if (isInRange) return;
		if (other.tag == "Player") { 
			isInRange = true;
			playerInRange = other.GetComponent<PlayerMovement>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			isInRange = false;
			playerInRange = null;
		}
	}

	private void TryToInteract()
	{
		if (!isInRange) return;

		if (Input.GetKeyDown(KeyCode.E) && playerInRange != null)
		{
			playerInRange.ToggleMovement(isBeingUsed);
			isBeingUsed = !isBeingUsed;
			computerGUI.SetActive(isBeingUsed);
			if (isBeingUsed)
			{
				playerInRange.GetThirdPersonCamera().ChangeCameraState(ThirdPersonCamera.CameraState.pivoted,cameraPivotPoint);
			}
			else
			{
				playerInRange.GetThirdPersonCamera().ChangeCameraState(
					ThirdPersonCamera.CameraState.following,
					playerInRange.transform,
					playerInRange.GetAnchor(),
					playerInRange.GetModel());
			}
		}
	}

	private void Update()
	{
		TryToInteract();
	}
}